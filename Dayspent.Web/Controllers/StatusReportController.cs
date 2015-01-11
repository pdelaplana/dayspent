using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Core.Repository.Commands;
using Dayspent.Web.Models;

namespace Dayspent.Web.Controllers
{
    [Authorize]
    public class StatusReportController : Controller
    {
        private IApplicationRepository _repository;

        public StatusReportController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        // GET: StatusReport
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            
            // check if current user has at least one status report, and if not create the first one
            int userReports = _repository.StatusReports.Count(s => s.ReportingUserId == userId);
            if (userReports == 0)
            {
                _repository.ExecuteCommand(new CreateStatusReportCommand
                {
                    ReportDate = DateTime.UtcNow,
                    ReportingUserId = userId
                });
            } 

            return PartialView("_index", new MyReportsViewModel{
                ReportCategories = AutoMapper.Mapper.Map<IList<StatusReportCategory>, IList<StatusReportCategoryViewModel>>(_repository.StatusReportCategories.OrderBy(c => c.Sequence).ToList()),
                StatusReports = AutoMapper.Mapper.Map<IList<StatusReport>, IList<StatusReportViewModel>>(_repository.StatusReports.Where(r => r.ReportingUserId.ToString() == userId).ToList())
            });
        }
    }
}