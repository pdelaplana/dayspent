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
    public class DeleteStatusReportItemCommand : IApplicationRepositoryCommand<bool>
    {
        public int StatusReportItemId { get; set; }

        public CommandResult<bool> Execute(ApplicationDb db)
        {
            StatusReportItem item = db.StatusReportItems.Find(this.StatusReportItemId);
            db.StatusReportItems.Remove(item);

            db.SaveChanges();
            
            return new CommandResult<bool> { Data = true, ResultCode = "0", ResultText = "Succesfully created status report item" };
            
        }
    }
}
