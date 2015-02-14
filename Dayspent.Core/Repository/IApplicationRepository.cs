using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayspent.Core.Models;

namespace Dayspent.Core.Repository
{
    public interface IApplicationRepository
    {
        void SeedDefaultData();

        CommandResult<T> ExecuteCommand<T>(IApplicationRepositoryCommand<T> command);

        T Get<T>(int id) where T : class;

        IQueryable<Timeline> Timelines { get; }

        IQueryable<Activity> Activities { get; }

        IQueryable<Tag> Tags { get; }

        IQueryable<StatusReport> StatusReports { get; }
        
        IQueryable<StatusReportItem> StatusReportItems { get; }

        IQueryable<StatusReportCategory> StatusReportCategories { get; }

        IQueryable<ReportingGroup> ReportingGroups { get; }

        IQueryable<Trackable> Trackables { get; }

    }
}
