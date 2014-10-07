using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;


namespace Dayspent.Web.API
{
    [RoutePrefix("api/tags")]
    [Authorize]
    public class TagController : ApiController
    {
        private IApplicationRepository _repository;

        public TagController(IApplicationRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public IDictionary<string, string> Get(string name)
        {
            return _repository.Tags.Where(t => t.Name.Contains(name)).ToDictionary(key => key.TagId.ToString(), value => value.Name);
        }

        [Route("all")]
        public IDictionary<string, string> GetAll()
        {
            return _repository.Tags.ToDictionary(key => key.TagId.ToString(), value => value.Name);
        }

        
    }
}
