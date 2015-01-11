using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class MyReportsViewModel
    {
        public IList<StatusReportCategoryViewModel> ReportCategories { get; set; }
        public IList<StatusReportViewModel> StatusReports { get; set; }

    }
}