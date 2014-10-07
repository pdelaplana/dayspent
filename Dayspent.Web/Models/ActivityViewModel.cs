using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string TimeSpent { get; set; }
        public int? TimeSpentMins { get; set; }
        public string ActivityByUserId { get; set; }
        public string ActivityByUserFullName { get; set; }
        public DateTime DateGroup { get; set; }
        public string TagGroup { get; set; }
        public string[] Tags { get; set; }

    }
}