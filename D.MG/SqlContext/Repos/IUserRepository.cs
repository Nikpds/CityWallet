using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SqlContext.Repos
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetByUsername(string username);

        Task<User> GetById(string id);

        bool InsertMany(List<User> entities);

        void Save();
    }
}
