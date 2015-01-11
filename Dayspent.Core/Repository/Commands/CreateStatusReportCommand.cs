using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;

namespace Dayspent.Core.Repository.Commands
{
    public class CreateStatusReportCommand : IApplicationRepositoryCommand<StatusReport>
    {
        public DateTime ReportDate { get; set; }
        public string ReportingUserId { get; set; }
        public DateTime? SubmittedDate { get; set; }
        
        public CommandResult<StatusReport> Execute(ApplicationDb db)
        {
            StatusReport statusReport = db.StatusReports.Create();
            statusReport.ReportingUserId = this.ReportingUserId;
            statusReport.ReportDate = this.ReportDate;
            statusReport.SubmittedDate = this.SubmittedDate;
            db.StatusReports.Add(statusReport);
            db.SaveChanges();
            
            return new CommandResult<StatusReport> { Data = statusReport, ResultCode = "0", ResultText = "A new status report has been created." };
            
        }
    }
}
