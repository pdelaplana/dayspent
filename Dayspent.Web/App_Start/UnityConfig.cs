using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using Unity.Mvc5;
using Unity.WebApi;
using Microsoft.AspNet.Identity.EntityFramework;
using Livefrog.Commons.Services;
using Livefrog.Commons.Unity;
using Dayspent.Core;
using Dayspent.Core.Repository;
using Dayspent.Security;
using Dayspent.Web.Application.Cache;


namespace Dayspent.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICacheService, WebCacheService>();

            
            container.RegisterType<ApplicationContext, ApplicationWebContext>("", new PerRequestLifetimeManager());
            container.RegisterType<IApplicationRepository, ApplicationRepository>();

            // cache
            container.RegisterType<UserCache<ApplicationUser>>(
                new InjectionConstructor(
                    container.Resolve<ICacheService>(),
                    new ApplicationSecurityContext() as IdentityDbContext<ApplicationUser>
                    )
                );
            container.RegisterType<TenantsCache>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
            
            // webapi
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}