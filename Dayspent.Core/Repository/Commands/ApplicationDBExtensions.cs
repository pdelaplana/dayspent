using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayspent.Core.Models;

namespace Dayspent.Core.Repository.Commands
{
    public static class ApplicationDBExtensions
    {
        public static void UpdateStatusItemTags(this ApplicationDb db, string[] hashTags, StatusReportItem item)
        {
            Tag tag;
            StatusReportItemTag statusReportItemTag;
            foreach (var hashTag in hashTags)
            {
                // check if hashtag is already part of tags collection of item
                tag = item.Tags.Where(t => t.Tag.Name == hashTag).Select(t => t.Tag).SingleOrDefault();
                if (tag == null) // no,
                {
                    tag = db.Tags.Where(t => t.Name == hashTag).SingleOrDefault();
                    if (tag == null)
                    {
                        // create the tag first
                        tag = db.Tags.Create();
                        tag.Name = hashTag;
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

            // remove tags 
            foreach (var itemTag in item.Tags.ToArray())
            {
                if (!hashTags.Contains(itemTag.Tag.Name))
                {
                    db.StatusReportItemTags.Remove(itemTag);
                    db.SaveChanges();

                }
            }
            
        }
    }
}
