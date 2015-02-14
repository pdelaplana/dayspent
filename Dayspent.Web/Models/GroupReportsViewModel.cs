using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class GroupReportsViewModel
    {
        public int ReportingGroupId { get; set; }
        public string Name { get; set; }

        public IList<StatusReportItemViewModel> ReportItems { get; set; }

        public IList<ReportingGroupMemberViewModel> Members { get; set; }
    }
}