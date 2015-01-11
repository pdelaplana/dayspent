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
    public class DeleteStatusReportItemTagCommand : IApplicationRepositoryCommand<bool>
    {
        public int StatusReportItemId { get; set; }
        public string TagName { get; set; }

        public CommandResult<bool> Execute(ApplicationDb db)
        {
            StatusReportItemTag itemTag = db.StatusReportItemTags.Where(t => t.Tag.Name == this.TagName && t.StatusReportItemId == this.StatusReportItemId).SingleOrDefault();
            db.StatusReportItemTags.Remove(itemTag);
            db.SaveChanges();
            
            return new CommandResult<bool> { Data = true, ResultCode = "0", ResultText = "Succesfully deleted status report item tag." };
            
        }
    }
}
