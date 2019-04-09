using Sooryen.Business.Interface;
using Sooryen.Data.Interface;
using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Business
{
    public class UserManager : IUserManager
    {
        private IUserRepository UserRepostiory { get; }

        public UserManager(IUserRepository userRepostiory)
        {
            UserRepostiory = userRepostiory;

        }

        public async Task<UserModel> UserLogin(UserModel user)
        {

            var userModel = new UserModel();
            if (string.IsNullOrEmpty(user.UserId) || string.IsNullOrEmpty(user.UserId))
            {
                return userModel;
            }


            var validUser = await UserRepostiory.UserLogin(user.UserId, user.Password);
            if (validUser == null)
            {
                return userModel;
            }

            return userModel;
        }


    }
}
