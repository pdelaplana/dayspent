using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class StatusReportCategoryViewModel
    {
        public int StatusReportCategoryId { get; set; }

        public int Sequence { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Remarks { get; set; }
    }
}