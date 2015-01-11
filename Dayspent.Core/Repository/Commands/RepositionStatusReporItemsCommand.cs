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
    public class RepositionStatusReporItemsCommand : IApplicationRepositoryCommand<bool>
    {
        public int StatusReportCategoryId { get; set; }
        public int[] StatusReportItemIds { get; set; }
        
        
        public CommandResult<bool> Execute(ApplicationDb db)
        {
            StatusReportItem item;
            int sequence = 1;
            foreach (var id in this.StatusReportItemIds)
            {
                item = db.StatusReportItems.Find(id);
                if (item != null)
                {
                    if (item.StatusReportCategoryId != this.StatusReportCategoryId)
                    {
                        item.StatusReportCategoryId = this.StatusReportCategoryId;
                    }
                    item.Sequence = sequence++;
                }
            }
            db.SaveChanges();
            
            return new CommandResult<bool> { Data = true, ResultCode = "0", ResultText = "Succesfully repositioned status report item" };
            
        }
    }
}
