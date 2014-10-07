using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Livefrog.Commons.Services;
using Dayspent.Security;

namespace Dayspent.Web.Application.Cache
{
    public class UserCache<T>  where T : IdentityUser 
    {
        private ICacheService _cacheService;
        private IdentityDbContext<T> _securityContext;

        private IDictionary<string, T> GetUsersDictionary()
        {
            var dictionary = _cacheService.Get<Dictionary<string, T>>();
            if (dictionary == null)
            {
                Init();
                dictionary = _cacheService.Get<Dictionary<string, T>>();
            }
            return dictionary;
        }

        public UserCache(ICacheService cacheService, IdentityDbContext<T> context)
        {
            this._cacheService = cacheService;
            this._securityContext  = context;
        }

        public void Init()
        {
            IDictionary<string, T> dictionary = new Dictionary<string, T>();
            foreach (var user in this._securityContext.Users.ToList())
            {
                dictionary.Add(user.Id, (T)user);
            }

            this._cacheService.Add(dictionary);
        }

        public void Refresh()
        {
            this._cacheService.Remove<Dictionary<string, T>>();
            Init();
        }

     
        public T Get()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return Get(userId);
        }

        public T Get(string userId)
        {
            T user = null;
            var dictionary = GetUsersDictionary();
            if (dictionary.ContainsKey(userId))
                user = dictionary[userId];
            if (user == null)
            {
                user = (T)this._securityContext.Users.Where(u => u.Id == userId).SingleOrDefault();
                if (user != null)
                {
                    dictionary.Add(user.UserName, user);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return user;
            }
        }

        public void Update(string userId)
        {
            T user = (T)this._securityContext.Users.Where(u => u.Id == userId).SingleOrDefault();
            if (user != null)
            {
                var dictionary = GetUsersDictionary();
                dictionary.Remove(user.Id);
                dictionary.Add(user.Id, user);
            }
        }

        public void Remove(string userId)
        {
            var dictionary = GetUsersDictionary();
            dictionary.Remove(userId);
        }

        public IList<T> GetAll()
        {
            var dictionary = GetUsersDictionary();
            return dictionary.Values.ToList();        
        }

    }
}