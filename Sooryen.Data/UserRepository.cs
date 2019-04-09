using Sooryen.Data.Interface;
using Sooryen.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Data
{
    public class UserRepository : IUserRepository
    {

        /// <summary>
        /// The unit of work
        /// </summary>
        private IUnitOfWork UnitOfWork { get; }

        private IGenericRepository<User> UserMasterRepository { get; }

        public UserRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            UserMasterRepository = UnitOfWork.GetRepository<User>();
        }


        public async Task<User> UserLogin(string userId, string password)
        {
            return await UserMasterRepository.GetSingleAsync(x => x.UserId == userId && x.Password == password && x.IsActive == true);
        }


        public async Task<User> GetUserDetail(string userId)
        {
            return await UserMasterRepository.FindAsync(x => x.UserId == userId);
        }

        public async Task<User> GetUser(int id)
        {
            return await UserMasterRepository.FindAsync(x => x.Id == id);
        }


        public void UpdateUser(User user)
        {
            UserMasterRepository.UpdateEntity(user);
        }

        public void AddUser(User user)
        {
            UserMasterRepository.AddEntity(user);
        }
    }
}
