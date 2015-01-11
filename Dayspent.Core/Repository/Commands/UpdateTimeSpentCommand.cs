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
    public class UpdateTimeSpentCommand : IApplicationRepositoryCommand<StatusReportItem>
    {

        public int StatusReportItemId { get; set; }
        public string TimeSpent { get; set; }
        
        public CommandResult<StatusReportItem> Execute(ApplicationDb db)
        {
            StatusReportItem item = db.StatusReportItems.Find(this.StatusReportItemId);

            int timeSpentInMins = 0;
            int timeSpentinSecsCurrent = 0; 
            // check for preceding plus sign
            if (this.TimeSpent.Substring(0, 1) == "+")
            {
                this.TimeSpent = this.TimeSpent.Substring(1, this.TimeSpent.Length - 1);
                timeSpentinSecsCurrent = item.TimeSpentInSecs.HasValue ? item.TimeSpentInSecs.Value : 0;
            } 
                    
            
            // see if we can parse the values 
            if (!Int32.TryParse(this.TimeSpent, out timeSpentInMins))
            {
                // parse timespent d, m, h
                foreach (Match match in Regex.Matches(this.TimeSpent, @"\d+?d"))
                {
                    var days = Int32.Parse(match.Value.Substring(0, match.Value.Length - 1));
                    timeSpentInMins = timeSpentInMins + (days * 24 * 60);
                }
                foreach (Match match in Regex.Matches(this.TimeSpent, @"\d+?h"))
                {
                    var hours = Int32.Parse(match.Value.Substring(0, match.Value.Length - 1));
                    timeSpentInMins = timeSpentInMins + (hours * 60);
                }
                foreach (Match match in Regex.Matches(this.TimeSpent, @"\d+?m"))
                {
                    var minutes = Int32.Parse(match.Value.Substring(0, match.Value.Length - 1));
                    timeSpentInMins = timeSpentInMins + minutes;
                }
            }

            item.TimeSpentInSecs = timeSpentinSecsCurrent +(timeSpentInMins*60);

            db.SaveChanges();
            
            return new CommandResult<StatusReportItem> { Data = item, ResultCode = "0", ResultText = "Succesfully updated existing status report item" };
            
        }
    }
}
