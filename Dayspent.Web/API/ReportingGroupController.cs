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
    [RoutePrefix("api/groups")]
    [Authorize]
    public class ReportingGroupController : ApiController
    {
        private IApplicationRepository _repository;

        public ReportingGroupController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IList<ReportingGroupViewModel> Get()
        {
            var reportingGroups = _repository.ReportingGroups;

            return AutoMapper.Mapper.Map<IList<ReportingGroup>, IList<ReportingGroupViewModel>>(reportingGroups.ToList());
        }

    }
}
