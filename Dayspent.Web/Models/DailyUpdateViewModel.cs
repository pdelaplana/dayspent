using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class UserSummaryViewModel
    {
        public string UserId { get; set; }
        public string UserFullName { get; set; }

        public int InProgressWork { get; set; }
        public int CompletedWork { get; set; }
        public int NotStartedWork { get; set; }

        public int Impediments { get; set; }
        public int RedFlags { get; set; }

        public int TimeSpentInSecs { get; set; }
        public int MaxTimeAvailableInHours { get; set; }
    }

    public class DailyUpdateViewModel
    {
        public IList<UserSummaryViewModel> UserSummaries { get; set; }
    }
}