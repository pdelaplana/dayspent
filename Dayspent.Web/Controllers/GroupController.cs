using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Web.Models;

namespace Dayspent.Web.Controllers
{
    [Authorize]
    public class GroupController : Controller
    {
        private IApplicationRepository _repository;

        public GroupController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        // GET: ReportingGroup
        public ActionResult Index()
        {
            var items = _repository.StatusReportItems.ToList();

            return PartialView("_index", new GroupReportsViewModel
            {
                Name = "All",
                ReportingGroupId = 0,
                ReportItems = AutoMapper.Mapper.Map<IList<StatusReportItem>, IList<StatusReportItemViewModel>>(items)
            });
        }
    }
}