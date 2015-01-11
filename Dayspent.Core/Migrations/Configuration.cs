namespace Dayspent.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Dayspent.Core.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Dayspent.Core.Models.ApplicationDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dayspent.Core.Models.ApplicationDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.StatusReportCategories.AddOrUpdate(
                ct => ct.Code,
                new StatusReportCategory { Sequence = 1, Code = "ONGOING", Description = "Ongoing Work", TenantId = 1, CreatedByUserId = "migration", CreatedDate = DateTime.UtcNow, ModifiedByUserId = "migration", ModifiedDate = DateTime.UtcNow },
                new StatusReportCategory { Sequence = 2, Code = "COMPLETED", Description = "Completed Work", TenantId = 1, CreatedByUserId = "migration", CreatedDate = DateTime.UtcNow, ModifiedByUserId = "migration", ModifiedDate = DateTime.UtcNow },
                new StatusReportCategory { Sequence = 3, Code = "UPCOMING", Description = "Upcoming Work", TenantId = 1, CreatedByUserId = "migration", CreatedDate = DateTime.UtcNow, ModifiedByUserId = "migration", ModifiedDate = DateTime.UtcNow },
                new StatusReportCategory { Sequence = 4, Code = "IMPEDIMENT", Description = "Challenges and Obstacles", TenantId = 1, CreatedByUserId = "migration", CreatedDate = DateTime.UtcNow, ModifiedByUserId = "migration", ModifiedDate = DateTime.UtcNow } 
                );

            context.SaveChanges();
        }
    }
}
