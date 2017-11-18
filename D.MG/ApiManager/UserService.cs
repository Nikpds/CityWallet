using ApiManager.Models;
using AuthProvider;
using Microsoft.EntityFrameworkCore;
using Models;
using SqlContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiManager
{
    public interface IUserService
    {
        Task<UserDto> GetUserWithCounters(string userId);
        Task<User> GetUser(string userId);
        Task<User> GetByUsername(string username);
        Task<Counter> GetUserCounters(string userId);
        Task<User> GetByVerification(string token);
        Task<User> GetUserByEmail(string email);
        void SendResetPwdEmail(User user);
        User ChangePassword(User user, PasswordReset pwd);
        User FirstLoginProcess(User user);
    }
    public class UserService : IUserService
    {
        private DbSet<User> _dbSet;
        private DataContext _ctx;
        private IEmailProvider _smtp;

        public UserService(DataContext ctx, IEmailProvider smtp)
        {
            _dbSet = ctx.Set<User>();
            _ctx = ctx;
            _smtp = smtp;
        }

        public UserService(DataContext ctx)
        {
            _dbSet = ctx.Set<User>();
            _ctx = ctx;
        }

        public async Task<UserDto> GetUserWithCounters(string userId)
        {
            var user = await _dbSet.Include(x => x.AddressInfo).Include(i => i.Bills).ThenInclude(t => t.Payment).SingleOrDefaultAsync(s => s.Id == userId);
            var bills = user.Bills;
            var counter = new Counter
            {
                Paid = bills.Where(w => w.Payment != null).Count(),
                PaidAmount = bills.Where(w => w.Payment != null).Sum(s => s.Amount),
                UnPaid = bills.Where(w => w.Payment == null && w.SettlementId == null).Count(),
                UnPaidAmount = bills.Where(w => w.Payment == null && w.SettlementId == null).Sum(s => s.Amount),
                OnSettle = bills.Where(w => w.SettlementId != null).Count(),
                OnSettleAmount = bills.Where(w => w.SettlementId != null).Sum(s => s.Amount),
                Payments = bills.Where(w => w.Payment != null).Count(),
                PaymentsAmount = bills.Where(w => w.Payment != null).Sum(s => s.Amount),
                Settlements = await _ctx.Set<Settlement>().Where(x => x.Bills.Any(a => a.UserId == userId)).CountAsync(),
                SettleAmount = bills.Where(w => w.SettlementId != null).Sum(s => s.Amount)
            };
            counter.Bills = counter.Paid + counter.UnPaid + counter.OnSettle;
            counter.BillsAmount = counter.PaidAmount + counter.UnPaidAmount + counter.OnSettleAmount;

            var userDto = new UserDto(user)
            {
                Counters = counter
            };
            return userDto;
        }

        public async Task<User> GetUser(string userId)
        {
            var user = await _dbSet.SingleOrDefaultAsync(s => s.Id == userId);

            return user;
        }

        public async Task<User> GetByUsername(string username)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Vat == username);

            return user;
        }

        public async Task<Counter> GetUserCounters(string userId)
        {
            var counter = new Counter
            {
                Bills = await _ctx.Set<Bill>().Where(x => x.UserId == userId).CountAsync(),
                Payments = await _ctx.Set<Payment>().Include(i => i.Bill).Where(x => x.Bill.UserId == userId).CountAsync(),
                Settlements = await _ctx.Set<Bill>().Where(x => x.UserId == userId && x.SettlementId != null).CountAsync()
            };

            return counter;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public void SendResetPwdEmail(User user)
        {
            user.VerificationToken = (Guid.NewGuid()).ToString();
            user.LastUpdate = DateTime.Now;

            _dbSet.Update(user);

            _ctx.SaveChanges();

            _smtp.SendResetPwdEmail(user);
        }

        public User ChangePassword(User user, PasswordReset pwd)
        {
            user.Password = PasswordHasher.HashPassword(pwd.NewPassword);
            user.LastUpdate = DateTime.Now;

            if (user.FirstLogin)
            {
                user.FirstLogin = false;
            }

            user.VerificationToken = null;

            _dbSet.Update(user);

            _ctx.SaveChanges();

            return user;
        }

        public async Task<User> GetByVerification(string token)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.VerificationToken == token);
        }

        public User FirstLoginProcess(User user)
        {
            user.VerificationToken = (Guid.NewGuid()).ToString();
            user.LastUpdate = DateTime.Now;

            _dbSet.Update(user);

            _ctx.SaveChanges();

            _smtp.FirstLogin(user);

            return user;
        }
    }
}
