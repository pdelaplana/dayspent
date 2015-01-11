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
    public class SubmitStatusReportCommand : IApplicationRepositoryCommand<StatusReport>
    {

        public int StatusReportId { get; set; }
        public DateTime SubmittedDate { get; set; }
        
        public CommandResult<StatusReport> Execute(ApplicationDb db)
        {
            // 
            StatusReport statusReport = db.StatusReports.Find(this.StatusReportId);
            if (this.SubmittedDate.Kind == DateTimeKind.Local || this.SubmittedDate.Kind == DateTimeKind.Unspecified)
                statusReport.SubmittedDate = this.SubmittedDate.ToUniversalTime();
            else
                statusReport.SubmittedDate = this.SubmittedDate;
            db.SaveChanges();
            
            // create 


            return new CommandResult<StatusReport> { Data = statusReport, ResultCode = "0", ResultText = "Succesfully submitted status report" };
            
        }
    }
}
