using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Core.Repository.Commands;
using Dayspent.Security;
using Dayspent.Web.Models;
using Dayspent.Web.Application.Cache;

namespace Dayspent.Web.API
{
    public class UserSummaryParam
    {
        public DateTime ReportDate { get; set; }
    }

    public class TagSummaryParam
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    [RoutePrefix("api/dashboard")]
    [Authorize]
    public class DashboardController : ApiController
    {
        private IApplicationRepository _repository;
        private UserCache<ApplicationUser> _userCache;

        public DashboardController(IApplicationRepository repository, UserCache<ApplicationUser> userCache)
        {
            _repository = repository;
            _userCache = userCache;
        }

        [Route("overview")]
        [HttpPost]
        public IList<UserSummaryViewModel> Overview([FromBody] UserSummaryParam param) 
        {
            //ApplicationUser user;
            // get user
            var applicationUser = _userCache.Get(User.Identity.GetUserId());

            // convert reportDate to UTC
            param.ReportDate = param.ReportDate.ToUniversalTime();

            var summaries = new List<UserSummaryViewModel>();

            foreach (var user in _userCache.GetAll())
            {
                summaries.Add(new UserSummaryViewModel
                {
                    UserId = user.Id,
                    UserFullName = user.FullName
                });

            }

            var query = _repository.StatusReportItems.Where(i => DbFunctions.DiffDays(i.StatusReport.ReportDate, param.ReportDate) == 0).GroupBy(i => i.StatusReport.ReportingUserId);
            UserSummaryViewModel userSummary;
            foreach (IGrouping<string, StatusReportItem> item in query.ToList())
            {

                userSummary = summaries.Where(s => s.UserId == item.Key).SingleOrDefault();

                userSummary.InProgressWork = item.Where(i => i.StatusReportCategory.Code == StatusReportCategoryCodes.InProgess).Count();
                userSummary.CompletedWork = item.Where(i => i.StatusReportCategory.Code == StatusReportCategoryCodes.Completed).Count();
                userSummary.NotStartedWork = item.Where(i => i.StatusReportCategory.Code == StatusReportCategoryCodes.NotStarted).Count();
                userSummary.Impediments = item.Where(i => i.StatusReportCategory.Code == StatusReportCategoryCodes.Impediment).Count();
                userSummary.TimeSpentInSecs = item.Sum(i => i.TimeSpentInSecs).Value;
                userSummary.MaxTimeAvailableInHours = 8;


                
            };
            return summaries;
        }

        [Route("tagsummary")]
        [HttpPost]
        public IList<StatusReportItemViewModel> TagSummary([FromBody] TagSummaryParam param)
        {
            var result = _repository.StatusReportItems.Where(i => i.StatusReport.ReportDate <= param.EndDate && i.StatusReport.ReportDate >= param.StartDate).ToList();
            return AutoMapper.Mapper.Map<IList<StatusReportItem>, IList<StatusReportItemViewModel>>(result);
        }

    }
}
