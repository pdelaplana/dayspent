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
    public class CreateStatusReportItemTagCommand : IApplicationRepositoryCommand<StatusReportItemTag>
    {
        public int StatusReportItemId { get; set; }
        public string TagName { get; set; }
        
        public CommandResult<StatusReportItemTag> Execute(ApplicationDb db)
        {
            Tag tag = db.Tags.Where(t => t.Name == TagName).SingleOrDefault();

            if (tag == null)
            {
                tag = db.Tags.Create();
                tag.Name = this.TagName;
                tag = db.Tags.Add(tag);
                db.SaveChanges();
            }

            StatusReportItemTag itemTag = db.StatusReportItemTags.Create();
            itemTag.StatusReportItemId = this.StatusReportItemId;
            itemTag.TagId = tag.TagId;
            db.StatusReportItemTags.Add(itemTag);
            db.SaveChanges();
            
            return new CommandResult<StatusReportItemTag> { Data =  itemTag, ResultCode = "0", ResultText = "Succesfully added status report item tag." };
            
        }
    }
}
