using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Dayspent.Security
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(RoleStore<IdentityRole> store)
            : base(store)
        {
            
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            //var roleManager = new RedfernRoleManager(new RoleStore<IdentityRole>(context.Get<RedfernSecurityContext>()));
            var roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationSecurityContext>()));
            return roleManager;
        }

        
    }
}
