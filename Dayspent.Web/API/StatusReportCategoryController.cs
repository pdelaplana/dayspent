using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Web.Models;

namespace Dayspent.Web.API
{
    [RoutePrefix("api/categories")]
    [Authorize]
    public class StatusReportCategoryController : ApiController
    {
        private IApplicationRepository _repository;

        public StatusReportCategoryController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IList<StatusReportCategoryViewModel> Get()
        {
            var reportCategories = _repository.StatusReportCategories;
            return AutoMapper.Mapper.Map<IList<StatusReportCategory>, IList<StatusReportCategoryViewModel>>(reportCategories.ToList());
        }


    }
}
