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
    public class CreateActivityCommand : IApplicationRepositoryCommand<Activity>
    {
        public int TimelineId { get; set; }
        public string Description { get; set; }
        public string TimeSpent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string[] Tags { get; set; }

        public CommandResult<Activity> Execute(ApplicationDb db)
        {
            Activity activity = db.Activities.Create();
            activity.ActivityByUserId = db.Context.ClientUserId;
            activity.TimelineId = this.TimelineId;
            activity.Description = this.Description;
            if (activity.StartDate.Kind == DateTimeKind.Local || activity.StartDate.Kind == DateTimeKind.Unspecified)
                activity.StartDate = this.StartDate.ToUniversalTime();
            else
                activity.StartDate = this.StartDate;

            if (activity.EndDate.HasValue && (activity.EndDate.Value.Kind == DateTimeKind.Local || activity.EndDate.Value.Kind == DateTimeKind.Unspecified))        
                activity.EndDate = this.EndDate;
            if (!String.IsNullOrEmpty(this.TimeSpent))
            {
                activity.TimeSpent = this.TimeSpent;
                int timeSpentInMins = 0;
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

                activity.TimeSpentMins = timeSpentInMins;
            }
            
            db.Activities.Add(activity);
            db.SaveChanges();

            if (this.Tags != null)
            {
                // add tags
                Tag tag;
                ActivityTag activityTag;
                foreach (var tagName in this.Tags)
                {
                    if (String.IsNullOrEmpty(tagName)) break;
                    tag = db.Tags.Where(t => t.Name == tagName).SingleOrDefault();
                    if (tag == null)
                    {
                        tag = db.Tags.Create();
                        tag.Name = tagName;
                        db.Tags.Add(tag);
                        db.SaveChanges();
                    }
                    activityTag = db.ActivityTags.Create();
                    activityTag.TagId = tag.TagId;
                    activityTag.ActivityId = activity.ActivityId;
                    db.ActivityTags.Add(activityTag);
                    db.SaveChanges();
                }

            }
            
            return new CommandResult<Activity> { Data = activity, ResultCode = "0", ResultText = "Succesfully created new activity" };
            
        }
    }
}
