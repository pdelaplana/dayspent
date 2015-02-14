using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Core.Repository.Commands;
using Dayspent.Security;
using Dayspent.Web.Models;
using Dayspent.Web.Application.Cache;

namespace Dayspent.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IApplicationRepository _repository;
        private UserCache<ApplicationUser> _userCache;

        public DashboardController(IApplicationRepository repository, UserCache<ApplicationUser> userCache)
        {
            _repository = repository;
            _userCache = userCache;
        }

        public ActionResult Index()
        {
            
            return PartialView("_index");
        }

        public ActionResult Overview()
        {
            return PartialView("_overview");
        }

        public ActionResult Tags()
        {
            return PartialView("_tags");
        }
    }
}