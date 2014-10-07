using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace Dayspent.Security
{
    public class ApplicationSecurityContext : IdentityDbContext<ApplicationUser>
    {

        public static ApplicationSecurityContext Create()
        {
            return new ApplicationSecurityContext();
        }

        public static UserManager<ApplicationUser> CreateUserManager()
        {
            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationSecurityContext()));
        }

        public IList<ApplicationUser> GetUsers()
        {
            return this.Users.ToList();
        }

        public ApplicationSecurityContext()
            : base("MembershipConnection", throwIfV1Schema: false)
        {
           
        }
    }
}
