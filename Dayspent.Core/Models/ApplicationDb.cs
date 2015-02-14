using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayspent.Core;
using Dayspent.Security;


namespace Dayspent.Core.Models
{
    public class ApplicationDb : DbContext
    {
        private ApplicationContext _appContext;
        private ApplicationSecurityContext _securityContext = new ApplicationSecurityContext();


        public static string CreateConnectionString(ApplicationContext context)
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = context.DbServer,
                InitialCatalog = context.DbName,
                UserID = context.DbUser,
                Password = context.DbPassword,
                PersistSecurityInfo = true,
                ApplicationName = "Dasypent",
            }.ConnectionString;

        }


        public ApplicationDb(ApplicationContext context) 
            : base(ApplicationDb.CreateConnectionString(context))
        {
            _appContext = context;

        }

        public ApplicationDb() : base("Name=AppDbConnection")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var utcDate = DateTime.UtcNow;
            var changeSet = ChangeTracker.Entries<Auditable>();
            if ((changeSet != null) && (_appContext != null))
            {

                foreach (DbEntityEntry<Auditable> entry in changeSet)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.TenantId = _appContext.TenantID;
                            entry.Entity.CreatedByUserId = _appContext.ClientUserId;
                            entry.Entity.CreatedDate = utcDate;
                            entry.Entity.ModifiedByUserId = _appContext.ClientUserId;
                            entry.Entity.ModifiedDate = utcDate;
                            break;
                        case EntityState.Modified:
                            entry.Entity.ModifiedByUserId = _appContext.ClientUserId;
                            entry.Entity.ModifiedDate = utcDate;
                            break;
                    }
                }
            }
            return base.SaveChanges();

        }

        
        public ApplicationContext Context { get { return _appContext; } }

        public DbSet<Timeline> Timelines { get; set; }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityTag> ActivityTags { get; set; }
        
        public DbSet<Tag> Tags { get; set; }

        public DbSet<StatusReport> StatusReports { get; set; }

        public DbSet<StatusReportItem> StatusReportItems { get; set; }
        public DbSet<StatusReportItemTag> StatusReportItemTags { get; set; }

        public DbSet<StatusReportCategory> StatusReportCategories { get; set; }

        public DbSet<ReportingGroup> ReportingGroups { get; set; }

        public DbSet<Trackable> Trackables { get; set; }
    }
}
