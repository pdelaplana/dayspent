using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Livefrog.Commons.Helpers;
using Dayspent.Core;
using Dayspent.Security;
using Dayspent.Web.Application.Cache;

namespace Dayspent.Web
{
    public class ApplicationWebContext : ApplicationContext
    {
        public ApplicationWebContext(TenantsCache tenantsCache, UserCache<ApplicationUser> userCache)
        {
            //TODO: Obtaining the client user name and IP address from HTTPContext makes an assumption that 
            //      HTTPRequest is always available,this is not always the e.g. Application Start.  As a workaround, 
            //      I have put in a try... catch block to handle situation where the HTTPRequest is not available.
            //      Futurewise, we need to refactor this to remove this dependency on the HTTPContext
            try
            {
                if ((HttpContext.Current.User != null) && (!String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))){
                    var user = userCache.Get(HttpContext.Current.User.Identity.GetUserId());
                    if (user != null)
                    {
                        this.ClientUserName = user.UserName;
                        this.ClientUserId = user.Id;
                        this.ClientUserFullName = user.FullName;
                    }
                }
                else 
                {
                    this.ClientUserName = "Anonymous";
                }
                    
                this.ClientIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                this.ClientTimeZone = ClientTimeZoneHelper.UserTimeZone;
                string host = HttpContext.Current.Request.Headers["Host"].Split(':')[0];

                var tenant = tenantsCache.GetTenantByHost(host);
                if (tenant == null)
                    // use default
                    tenant = tenantsCache.GetTenantByName("Default");

                this.TenantID = tenant.TenantId;
                this.DbServer = tenant.DbServer;
                this.DbName = tenant.DbName;
                this.DbUser = tenant.DbUser;
                this.DbPassword = tenant.DbPassword;
                //this.Metadata = @"res://*/Models.TractionModel.csdl|res://*/Models.TractionModel.ssdl|res://*/Models.TractionModel.msl";
            }
            catch
            {

            }
            
        }
    }
}