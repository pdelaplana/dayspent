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

    [RoutePrefix("api/activity/{activityid:int}/tags")]
    [Authorize]
    public class ActivityTagController : ApiController
    {
        private IApplicationRepository _repository;

        public ActivityTagController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public WebApiResult<ActivityTagViewModel> Post([FromBody] CreateActivityTagCommand command ) 
        {
            var result = _repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<ActivityTag>, WebApiResult<ActivityTagViewModel>>(result);
        }

        [Route("{name:alpha}")]
        public WebApiResult<bool> Delete(int activityid, string name)
        {
            var result = _repository.ExecuteCommand(new DeleteActivityTagCommand { ActivityId = activityid, TagName = name });
            return AutoMapper.Mapper.Map<CommandResult<bool>, WebApiResult<bool>>(result);
        }
        
    }
}
