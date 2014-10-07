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
    public class DeleteActivityTagCommand : IApplicationRepositoryCommand<bool>
    {
        public int ActivityId { get; set; }
        public string TagName { get; set; }

        public CommandResult<bool> Execute(ApplicationDb db)
        {
            ActivityTag activityTag = db.ActivityTags.Where(at => at.Tag.Name == this.TagName && at.ActivityId == this.ActivityId).SingleOrDefault();
            db.ActivityTags.Remove(activityTag);
            db.SaveChanges();
            
            return new CommandResult<bool> { Data = true, ResultCode = "0", ResultText = "Succesfully deleted activity tag." };
            
        }
    }
}
