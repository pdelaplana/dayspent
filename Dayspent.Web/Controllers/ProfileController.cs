using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Dayspent.Security;
using Dayspent.Web.Models;

namespace Dayspent.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        //
        // GET: /profile/
        public ActionResult Index()
        {
            var userAccount = UserManager.FindById(User.Identity.GetUserId());
            var model = AutoMapper.Mapper.Map<ApplicationUser, ProfileViewModel>(userAccount);
            return PartialView("_index", model);
        }
    }
}