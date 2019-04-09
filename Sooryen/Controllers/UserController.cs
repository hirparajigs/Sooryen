using Sooryen.Business.Interface;
using Sooryen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sooryen.Controllers
{
    public class UserController : Controller
    {


        public IUserManager UserManager { get; }

        public UserController(IUserManager userManager)
        {
            UserManager = userManager;
        }


        [HttpPost]

        public async Task<ActionResult> UserLogin(UserModel user)
        {
            var userDetail = await UserManager.UserLogin(user);
            return View(userDetail);
        }

    }
}