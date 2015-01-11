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

    [RoutePrefix("api/report/{id:int}/items/{itemid:int}/tags")]
    [Authorize]
    public class StatusReportItemTagController  : ApiController
    {
        private IApplicationRepository _repository;

        public StatusReportItemTagController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public WebApiResult<StatusReportItemTagViewModel> Post([FromBody] CreateStatusReportItemTagCommand command ) 
        {
            var result = _repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<StatusReportItemTag>, WebApiResult<StatusReportItemTagViewModel>>(result);
        }

        [Route("{name}")]
        public WebApiResult<bool> Delete(int itemid, string name)
        {
            var result = _repository.ExecuteCommand(new DeleteStatusReportItemTagCommand { StatusReportItemId = itemid, TagName = name });
            return AutoMapper.Mapper.Map<CommandResult<bool>, WebApiResult<bool>>(result);
        }
        
    }
}
