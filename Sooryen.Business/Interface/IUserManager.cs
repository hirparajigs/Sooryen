using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sooryen.Business.Interface
{
    public interface IUserManager
    {
         Task<UserModel> UserLogin(UserModel user);
    }
}
