using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayspent.Core;
using Dayspent.Core.Models;
using Dayspent.Security;

namespace Dayspent.Core.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        ApplicationContext _context;
        ApplicationDb _db;
       
        public ApplicationRepository(ApplicationContext context)
        {
            _db = new ApplicationDb(context);
        }

        public void SeedDefaultData()
        {
            
        }

        public CommandResult<T> ExecuteCommand<T>(IApplicationRepositoryCommand<T> command)
        {
            return command.Execute(_db);
        }

        public T Get<T>(int id) where T : class
        {
            return _db.Set<T>().Find(id);
        }

        public IQueryable<Timeline> Timelines
        {
            get { return _db.Timelines.Where(tl => tl.TenantId == _db.Context.TenantID).AsQueryable(); }
        }

        public IQueryable<Activity> Activities
        {
            get { return _db.Activities.Where(tc => tc.TenantId == _db.Context.TenantID).AsQueryable();  }
        }

        public IQueryable<Tag> Tags
        {
            get { return _db.Tags.Where(t => t.TenantId == _db.Context.TenantID).AsQueryable(); }
        }


        public IQueryable<StatusReport> StatusReports
        {
            get { return _db.StatusReports.Where(t => t.TenantId == _db.Context.TenantID).AsQueryable(); }
        }

        public IQueryable<StatusReportItem> StatusReportItems
        { 
            get { return _db.StatusReportItems.Where(i =>i.StatusReport.TenantId == _db.Context.TenantID).AsQueryable(); } 
        }

        public IQueryable<StatusReportCategory> StatusReportCategories
        {
            get { return _db.StatusReportCategories.Where(t => t.TenantId == _db.Context.TenantID).AsQueryable(); }
        }
        
    }
}
