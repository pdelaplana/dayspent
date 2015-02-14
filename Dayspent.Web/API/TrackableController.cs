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
    [RoutePrefix("api/trackables")]
    [Authorize]
    public class TrackableController : ApiController
    {
        private IApplicationRepository _repository;

        public TrackableController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IList<TrackableViewModel> Get(string description)
        {
            return AutoMapper.Mapper.Map<IList<Trackable>, IList<TrackableViewModel>>(_repository.Trackables.Where(t => t.Description.Contains(description)).ToList());
        }

        [Route("")]
        public WebApiResult<TrackableViewModel> Post(CreateTrackableCommand command) 
        {
            var result = this._repository.ExecuteCommand(command);
            return AutoMapper.Mapper.Map<CommandResult<Trackable>, WebApiResult<TrackableViewModel>>(result);
        
        }

    }
}
