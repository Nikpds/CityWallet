using Microsoft.EntityFrameworkCore;
using SqlContext;
using ApiManager;

namespace Testing
{
    public class Program
    {

        static void Main(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();

            builder.UseSqlServer("Server=DESKTOP-L9O20VR;Database=qualco4;Integrated Security=true;");

            var _db = new UnitOfWork(new DataContext(builder.Options));
            var users = UserManager.InsertUsers();

            var count = users.Count;

            _db.UserRepository.InsertMany(users);
            _db.Save();

        }
    }

}
