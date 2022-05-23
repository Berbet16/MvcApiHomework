using Models;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using DAL;

namespace WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<ILogger, LoggerForPostgre>();
            //container.RegisterType<ILogger, LoggerForSQLServer>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}