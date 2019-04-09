using Sooryen.Data;
using Sooryen.Data.Interface;
using Microsoft.Practices.Unity;

namespace Sooryen.Business
{
    public class BusinessUnityModule : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            Container.RegisterType<IUserRepository, UserRepository>();
            Container.RegisterType<INoteRepository, NoteRepository>();
        }
    }
}
