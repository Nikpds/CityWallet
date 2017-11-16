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
            var user = await _dbSet.Include(x => x.AddressInfo).SingleOrDefaultAsync(s => s.Id == userId);
            var counter = new Counter
            {
                Bills = await _ctx.Set<Bill>().Where(x => x.UserId == userId && x.Payment == null && x.SettlementId == null).CountAsync(),
                Payments = await _ctx.Set<Payment>().Include(i => i.Bill).Where(x => x.Bill.UserId == userId).CountAsync(),
                Settlements = await _ctx.Set<Settlement>().Where(x => x.Bills.Any(a => a.UserId == userId)).CountAsync()
            };

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

            _dbSet.Update(user);

            _ctx.SaveChanges();

            _smtp.SendResetPwdEmail(user);
        }

        public User ChangePassword(User user, PasswordReset pwd)
        {
            user.Password = PasswordHasher.HashPassword(pwd.NewPassword);

            user.VerificationToken = null;

            _dbSet.Update(user);

            _ctx.SaveChanges();

            return user;
        }

        public async Task<User> GetByVerification(string token)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.VerificationToken == token);
        }

    }
}
