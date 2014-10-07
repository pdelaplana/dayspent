﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Livefrog.Commons.Services;
using Livefrog.Tenants.Db;

namespace Dayspent.Web.Application.Cache
{
    public class TenantsCache
    {
        private ICacheService _cacheService;
        private TenantDb _tenantDb;

        
        private IList<Tenant> GetTenantsList()
        {
            var listOfTenants = _cacheService.Get<List<Tenant>>();
            if (listOfTenants == null)
            {
                _cacheService.Add(_tenantDb.Tenants.ToList());
                listOfTenants = _cacheService.Get<List<Tenant>>();
            }
            return listOfTenants;
        }

        public TenantsCache(ICacheService cacheService)
        {
            this._cacheService = cacheService;
            this._tenantDb = new TenantDb();
        }

        public void Init()
        {
            this._cacheService.Add(this._tenantDb.Tenants.ToList());
        }

        public void Refresh()
        {
            _cacheService.Remove<List<Tenant>>();
            Init();
        }

        public IList<Tenant> GetAllTenants()
        {
            return GetTenantsList();
        }


        public Tenant GetTenantByHost(string host)
        {
            return GetTenantsList().Where(t => t.Host == host).SingleOrDefault();
        }

        public Tenant GetTenantByName(string name)
        {
            return GetTenantsList().Where(t => t.Name == name).SingleOrDefault();
        }

        public Tenant GetTenant(int tenantId)
        {
            return GetTenantsList().Where(t => t.TenantId == tenantId).SingleOrDefault();
        }

        public Tenant UpdateTenant(Tenant tenant)
        {
            var thisTenant = this._tenantDb.Tenants.Where(t => t.TenantId == tenant.TenantId).SingleOrDefault();
            if (thisTenant != null)
            {
                thisTenant.Name = tenant.Name;
                thisTenant.Status = tenant.Status;

                
            }
            else
            {
                this._tenantDb.Tenants.Add(tenant);
            }
                
            
            this._tenantDb.SaveChanges();
            GetTenantsList().Remove(tenant);
            GetTenantsList().Add(tenant);
            return GetTenant(tenant.TenantId);
        }

    }
}