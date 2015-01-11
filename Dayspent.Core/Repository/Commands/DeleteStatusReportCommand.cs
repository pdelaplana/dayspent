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
    public class DeleteStatusReportCommand : IApplicationRepositoryCommand<bool>
    {
        public int StatusReportId { get; set; }

        public CommandResult<bool> Execute(ApplicationDb db)
        {
            StatusReport statusReport = db.StatusReports.Find(this.StatusReportId);
            db.StatusReports.Remove(statusReport);
            db.SaveChanges();
            
            return new CommandResult<bool> { Data = true, ResultCode = "0", ResultText = "Succesfully deleted status report" };
            
        }
    }
}
