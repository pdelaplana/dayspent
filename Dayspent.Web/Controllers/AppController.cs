using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Dayspent.Core.Repository;
using Dayspent.Security;
using Dayspent.Web.Application;
using Dayspent.Web.Application.Cache;


namespace Dayspent.Web.Controllers
{
    [Authorize]
    public class AppController : Controller
    {

        private ApplicationWebContext _context;
        private UserCache<ApplicationUser> _userCache;
        private TenantsCache _tenantsCache;
        private IApplicationRepository _repository;

        public AppController(IApplicationRepository repository,
          ApplicationWebContext context,
          UserCache<ApplicationUser> userCache,
          TenantsCache tenantsCache)
        {
            _context = context;
            _tenantsCache = tenantsCache;
            _userCache = userCache;
            _repository = repository;
        }

        // GET: App
        public ActionResult Index()
        {
            // get appliction user
            var applicationUser = _userCache.Get(User.Identity.GetUserId());

            // set viewbag and cookie for frontend use
            ViewBag.AuthenticatedUser = AutoMapper.Mapper.Map<ApplicationUser, AuthenticatedUser>(applicationUser);
            HttpCookie cookie = new HttpCookie("AuthenticatedUser");
            cookie.Value = JsonConvert.SerializeObject(ViewBag.AuthenticatedUser, new JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() });
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            
            return View();
        }
    }
}