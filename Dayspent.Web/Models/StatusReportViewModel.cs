using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class StatusReportViewModel
    {
        public int StatusReportId { get; set; }

        public DateTime ReportDate { get; set; }

        public Guid ReportingUser { get; set; }
        public string ReportingUserFullName { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string Suggestions { get; set; }

        public IList<StatusReportItemViewModel> StatusReportItems { get; set; }
    }
}