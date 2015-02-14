using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Core.Repository.Commands;
using Dayspent.Web.Models;

namespace Dayspent.Web.API
{
    [RoutePrefix("api/reports")]
    [Authorize]
    public class StatusReportController : ApiController
    {
        private IApplicationRepository _repository;

        public StatusReportController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("{userId}")]
        public IList<StatusReportViewModel> Get(string userId) 
        {
            var statusReports = _repository.StatusReports;

            return AutoMapper.Mapper.Map<IList<StatusReport>, IList<StatusReportViewModel>>(statusReports.ToList());
        }

        [Route("{id}")]
        public WebApiResult<StatusReportViewModel> Put([FromBody] UpdateStatusReportCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<StatusReport>, WebApiResult<StatusReportViewModel>>(result);
        }

        [Route("{id}/submit")]
        [HttpPut]
        public WebApiResult<StatusReportViewModel> Submit([FromBody] SubmitStatusReportCommand command)
        {
            // submit status report
            this._repository.ExecuteCommand(command);

            // create new status report
            var result = this._repository.ExecuteCommand(new CreateStatusReportCommand{
                ReportDate = DateTime.UtcNow.Date,
                ReportingUserId = User.Identity.GetUserId()
            });

            // move ongoing and upcoming items from old to new report
            var items = this._repository.StatusReportItems.Where(i => i.StatusReportId == command.StatusReportId && (i.StatusReportCategory.Code == StatusReportCategoryCodes.InProgess || i.StatusReportCategory.Code == StatusReportCategoryCodes.NotStarted)).ToList();
            foreach (var item in items)
            {
                this._repository.ExecuteCommand(new CreateStatusReportItemCommand
                {
                    StatusReportId = result.Data.StatusReportId,
                    StatusReportCategoryId = item.StatusReportCategoryId,
                    Description = item.Description
                });
            }
 

            return AutoMapper.Mapper.Map<CommandResult<StatusReport>, WebApiResult<StatusReportViewModel>>(result);
        }

        [Route("{id}")]
        public WebApiResult<bool> Delete(int id)
        {
            var result = this._repository.ExecuteCommand(new DeleteStatusReportCommand { StatusReportId = id});
            return AutoMapper.Mapper.Map<CommandResult<bool>, WebApiResult<bool>>(result);
        }

        [Route("{id}/reposition")]
        [HttpPut]
        public WebApiResult<bool> Reposition([FromBody]RepositionStatusReporItemsCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<bool>, WebApiResult<bool>>(result);
        }
        
    }
}
