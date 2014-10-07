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
    public class CreateTimelineCommand : IApplicationRepositoryCommand<Timeline>
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
        
        public CommandResult<Timeline> Execute(ApplicationDb db)
        {
            Timeline timeline = db.Timelines.Create();
            timeline.Name = this.Name;
            timeline.OwnerId = this.OwnerId;
            db.Timelines.Add(timeline);
            db.SaveChanges();
            
            
            return new CommandResult<Timeline> { Data = timeline, ResultCode = "0", ResultText = "New timeline has been created" };
            
        }
    }
}
