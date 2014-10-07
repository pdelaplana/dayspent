using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Dayspent.Security;
using Dayspent.Web.Application;
using Dayspent.Web.Application.Cache;

namespace Dayspent.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
            AutoMapperConfig.RegisterMaps();

            // initialize user cache
            var userCache = DependencyResolver.Current.GetService<UserCache<ApplicationUser>>();
            userCache.Init();

            // initialize tenant Cache
            var tenantCache = DependencyResolver.Current.GetService<TenantsCache>();
            tenantCache.Init();
        }
    }
}
