using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Core.Repository.Commands;
using Dayspent.Web.Models;


namespace Dayspent.Web.API
{
    [RoutePrefix("api/report/{id}/items")]
    [Authorize]
    public class StatusReportItemController : ApiController
    {
        private IApplicationRepository _repository;

        public StatusReportItemController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        // POST /api/report/1/items
        [Route("")]
        public WebApiResult<StatusReportItemViewModel> Post([FromBody] CreateStatusReportItemCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<StatusReportItem>, WebApiResult<StatusReportItemViewModel>>(result);
        }


        // DELETE /api/report/1/items/1
        [Route("{itemid:int}")]
        public WebApiResult<bool> Delete(int itemid)
        {
            var result = this._repository.ExecuteCommand(new DeleteStatusReportItemCommand { StatusReportItemId = itemid });
            return AutoMapper.Mapper.Map<CommandResult<bool>, WebApiResult<bool>>(result);
        }

        // PUT /api/report/1/items/1
        [Route("{itemid:int}")]
        public WebApiResult<StatusReportItemViewModel> Put([FromBody] UpdateStatusReportItemCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<StatusReportItem>, WebApiResult<StatusReportItemViewModel>>(result);
        }

        // PUT /api/report/1/items/1/timespent
        [Route("{itemid:int}/timespent")]
        [HttpPut]
        public WebApiResult<StatusReportItemViewModel> TimeSpent([FromBody] UpdateTimeSpentCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<StatusReportItem>, WebApiResult<StatusReportItemViewModel>>(result);
        }

    }
}
