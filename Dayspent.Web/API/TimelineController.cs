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
    [RoutePrefix("api/timeline")]
    [Authorize]
    public class TimelineController : ApiController
    {
        private IApplicationRepository _repository;

        public TimelineController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public TimelineViewModel Get()
        {
            string userId = User.Identity.GetUserId();
            var timeline = _repository.Timelines.Where(tl => tl.OwnerId == userId && tl.Name == "My Timeline").SingleOrDefault();
            if (timeline == null)
            {
                var result = _repository.ExecuteCommand(new CreateTimelineCommand { Name = "My Timeline", OwnerId = userId });
                timeline = result.Data;
            }
            return AutoMapper.Mapper.Map<Timeline, TimelineViewModel>(timeline);
        }

    }
}
