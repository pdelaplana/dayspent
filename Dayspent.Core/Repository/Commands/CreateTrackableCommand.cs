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
    public class CreateTrackableCommand : IApplicationRepositoryCommand<Trackable>
    {
        public string Description { get; set; }
        
        public CommandResult<Trackable> Execute(ApplicationDb db)
        {
            Trackable trackable = db.Trackables.Create();
            trackable.Description = this.Description;
            db.Trackables.Add(trackable);
            db.SaveChanges();
            
            return new CommandResult<Trackable> { Data = trackable, ResultCode = "0", ResultText = "A new trackable has been created." };
            
        }
    }
}
