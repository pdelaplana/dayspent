using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dayspent.Web.Controllers
{
    public class TimelineController : Controller
    {
        // GET: timeline
        public ActionResult Index()
        {
            return PartialView("_index");
        }

        public ActionResult Dashboard() 
        {
            return PartialView("_dashboard");
        }

        public ActionResult ActivityStream() 
        {
            return PartialView("_activitystream");
        }

        public ActionResult Reports() 
        {
            return PartialView("_reports");
        }
    }
}