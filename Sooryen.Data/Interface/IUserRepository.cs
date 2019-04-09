using Sooryen.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Data.Interface
{
    public interface IUserRepository
    {
        Task<User> UserLogin(string userId, string Password);

        Task<User> GetUserDetail(string userId);

        Task<User> GetUser(int id);
        void UpdateUser(User user);

        void AddUser(User user);


    }

}
