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

            // and create new status report
            var result = this._repository.ExecuteCommand(new CreateStatusReportCommand{
                ReportDate = DateTime.UtcNow,
                ReportingUserId = User.Identity.GetUserId()
            });
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
