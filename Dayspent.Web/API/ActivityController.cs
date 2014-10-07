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

    public class TimelinePeriod
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
    
    [RoutePrefix("api/activities")]
    [Authorize]
    public class ActivityController : ApiController
    {
        private IApplicationRepository _repository;

        public ActivityController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        // GET /api/activities
        [Route("")]
        public IList<ActivityViewModel> Get([FromUri] TimelinePeriod period = null, int? timelineId = null)
        {
            var activities = this._repository.Activities;
            if (timelineId.HasValue)
            {
                activities = activities.Where(a => a.TimelineId == timelineId.Value);
            }
            else
            {
                // get the default timeline for the logged in user
                string userId = User.Identity.GetUserId();
                var timeline = this._repository.Timelines.Where(tl => tl.OwnerId == userId && tl.Name == "My Timeline").SingleOrDefault();
                if (timeline == null)
                {
                    // no default timeline, create one
                    var result = this._repository.ExecuteCommand(new CreateTimelineCommand { Name = "My Timeline", OwnerId = User.Identity.GetUserId() });
                    timeline = result.Data;
                }
                activities = activities.Where(a => a.TimelineId == timeline.TimelineId);
            }

            // TODO: check and filter for tags
            if (period != null && period.From.HasValue && period.To.HasValue)
            {
                if (period.From.HasValue)
                    period.From = period.From.Value.ToUniversalTime();
                if (period.To.HasValue)
                    period.To = period.To.Value.ToUniversalTime();
                activities = activities.Where(a => a.StartDate >= period.From && a.StartDate <= period.To);
            }

            return AutoMapper.Mapper.Map<IList<Activity>, IList<ActivityViewModel>>(activities.OrderByDescending(a => a.StartDate).ToList());
        }

        // POST /api/activities
        [Route("")]
        public WebApiResult<ActivityViewModel> Post([FromBody] CreateActivityCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<Activity>, WebApiResult<ActivityViewModel>>(result);
        }

        // PUT /api/activities/12
        [Route("{id:int}")]
        public WebApiResult<ActivityViewModel> Put(int id, [FromBody] UpdateActivityCommand command)
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<Activity>, WebApiResult<ActivityViewModel>>(result);
        }

        // DELETE /api/activities/12
        [Route("{id:int}")]
        public WebApiResult<bool> Delete(int id)
        {
            var result = this._repository.ExecuteCommand(new DeleteActivityCommand { ActivityId = id });
            return AutoMapper.Mapper.Map<CommandResult<bool>, WebApiResult<bool>>(result);
        }


    }
}
