using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Dayspent.Security;
using Dayspent.Web.Models;

namespace Dayspent.Web.API
{
    [RoutePrefix("api/profile")]
    [Authorize]
    public class ProfileController : ApiController
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ProfileController()
        {
            
            
        }
        

        // GET api/profile
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        // PUT api/profile/
        [Route("")]
        public Task<IdentityResult> Put([FromBody]ProfileViewModel model)
        {
            var user = UserManager.FindByName(model.UserName);
            user.FullName = model.FullName;
            user.Email = model.Email;
            return UserManager.UpdateAsync(user);
        }

        [Route("changepassword")]
        public Task<IdentityResult> ChangePassword([FromBody]ChangePasswordViewModel model) 
        {
            return UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword); 

        }

        
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}