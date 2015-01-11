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
    public class CreateStatusReportItemCommand : IApplicationRepositoryCommand<StatusReportItem>
    {
        public int StatusReportId { get; set; }
        public int StatusReportCategoryId { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }

        public CommandResult<StatusReportItem> Execute(ApplicationDb db)
        {
            var statusReportItems = db.StatusReports.Find(this.StatusReportId).StatusReportItems.ToList();
            
            StatusReportItem item = db.StatusReportItems.Create();
            item.StatusReportId = this.StatusReportId;
            item.StatusReportCategoryId = this.StatusReportCategoryId;
            item.Description = this.Description;
            item.Sequence = statusReportItems.Count > 0 ? statusReportItems.Max(i => i.Sequence) + 1 : 1; 
            
            db.StatusReportItems.Add(item);
            db.SaveChanges();

            // add tags from hashtags in description 
            var hashTags = this.Description.GetHashTags();
            db.UpdateStatusItemTags(hashTags, item);
            
            Tag tag;
            StatusReportItemTag statusReportItemTag;
            
            // add tags from array
            if (this.Tags != null)
            {
                foreach (var tagName in this.Tags)
                {
                    tag = db.Tags.Where(t => t.Name == tagName).SingleOrDefault();
                    if (tag == null)
                    {
                        // create the tag first
                        tag = db.Tags.Create();
                        tag.Name = tagName;
                        db.Tags.Add(tag);
                        db.SaveChanges();
                    }
                    statusReportItemTag = db.StatusReportItemTags.Create();
                    statusReportItemTag.TagId = tag.TagId;
                    statusReportItemTag.StatusReportItemId = item.StatusReportItemId;

                    db.StatusReportItemTags.Add(statusReportItemTag);
                    db.SaveChanges();
                }
            }
            
            return new CommandResult<StatusReportItem> { Data =  item, ResultCode = "0", ResultText = "Succesfully new report item." };
            
        }
    }
}
