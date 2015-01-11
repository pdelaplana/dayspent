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
    public class UpdateStatusReportCommand : IApplicationRepositoryCommand<StatusReport>
    {

        public int StatusReportId { get; set; }
        public DateTime ReportDate { get; set; }
        
        public CommandResult<StatusReport> Execute(ApplicationDb db)
        {
            StatusReport statusReport = db.StatusReports.Find(this.StatusReportId);
            if (this.ReportDate.Kind == DateTimeKind.Local || this.ReportDate.Kind == DateTimeKind.Unspecified)
                statusReport.ReportDate = this.ReportDate.ToUniversalTime();
            else
                statusReport.ReportDate = this.ReportDate;

            db.SaveChanges();
            
            return new CommandResult<StatusReport> { Data = statusReport, ResultCode = "0", ResultText = "Succesfully updated existing status repot" };
            
        }
    }
}
