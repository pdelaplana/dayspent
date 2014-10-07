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
    public class CreateActivityTagCommand : IApplicationRepositoryCommand<ActivityTag>
    {
        public int ActivityId { get; set; }
        public string TagName { get; set; }
        
        public CommandResult<ActivityTag> Execute(ApplicationDb db)
        {
            Tag tag = db.Tags.Where(t => t.Name == TagName).SingleOrDefault();

            if (tag == null)
            {
                tag = db.Tags.Create();
                tag.Name = this.TagName;
                tag = db.Tags.Add(tag);
                db.SaveChanges();
            }

            ActivityTag activityTag = db.ActivityTags.Create();
            activityTag.ActivityId = this.ActivityId;
            activityTag.TagId = tag.TagId;
            db.ActivityTags.Add(activityTag);
            db.SaveChanges();
            
            return new CommandResult<ActivityTag> { Data =  activityTag, ResultCode = "0", ResultText = "Succesfully added activity tag." };
            
        }
    }
}
