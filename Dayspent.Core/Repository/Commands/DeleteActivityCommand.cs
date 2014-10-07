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
    public class DeleteActivityCommand : IApplicationRepositoryCommand<bool>
    {
        public int ActivityId { get; set; }

        public CommandResult<bool> Execute(ApplicationDb db)
        {
            Activity activity = db.Activities.Find(this.ActivityId);
            db.Activities.Remove(activity);
            db.SaveChanges();
            
            return new CommandResult<bool> { Data = true, ResultCode = "0", ResultText = "Succesfully created new activity" };
            
        }
    }
}
