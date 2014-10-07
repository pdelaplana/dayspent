using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Dayspent.Security;
using Dayspent.Web.Application.Cache;

namespace Dayspent.Web.Application.Configuration.Automapper.Resolvers
{
    public class CacheUserFullNameResolver : ValueResolver<string, string>
    {
        private UserCache<ApplicationUser> _cache;

        public CacheUserFullNameResolver(UserCache<ApplicationUser>  cache)
        {
            _cache = cache;
        }

        protected override string ResolveCore(string source)
        {
            if (!String.IsNullOrEmpty(source))
                return ((ApplicationUser)_cache.Get(source)).FullName;
            else
                return "";
        }
    }
}