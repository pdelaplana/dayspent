using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dayspent.Core.Models;
using Dayspent.Core.Repository;
using Dayspent.Core.Utils;

namespace Dayspent.Core.Repository.Commands
{
    public class UpdateStatusReportItemCommand : IApplicationRepositoryCommand<StatusReportItem>
    {

        public int StatusReportItemId { get; set; }
        public string Description { get; set; }
        
        public CommandResult<StatusReportItem> Execute(ApplicationDb db)
        {
            StatusReportItem item = db.StatusReportItems.Find(this.StatusReportItemId);
            item.Description = this.Description;
            db.SaveChanges();

            db.UpdateStatusItemTags(this.Description.GetHashTags(), item);
            
            return new CommandResult<StatusReportItem> { Data = item, ResultCode = "0", ResultText = "Succesfully updated existing status report item" };
            
        }
    }
}
