using Sooryen.Business;
using Sooryen.Business.Interface;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;


namespace Sooryen
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            
            container.RegisterType<INoteManager, NoteManager>();
            container.RegisterType<IUserManager, UserManager>();
            container.AddNewExtension<BusinessUnityModule>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}