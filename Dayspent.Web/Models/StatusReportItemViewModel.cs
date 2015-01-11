using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class StatusReportItemViewModel
    {
        public int StatusReportItemId { get; set; }
        public int StatusReportId { get; set; }

        public string ReportingUserId { get; set; }
        public string ReportingUserFullName { get; set; }

        public int StatusReportCategoryId { get; set; }
        public string StatusReportCategoryDescription { get; set; }
        public string StatusReportCategoryCode { get; set; }

        public string Description { get; set; }

        public int? TimeSpentInSecs { get; set; }

        public int Sequence { get; set; }

        public bool HasRedFlag { get; set; }

        public string[] Tags { get; set; }

    }
}