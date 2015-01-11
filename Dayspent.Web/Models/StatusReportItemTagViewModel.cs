using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class StatusReportItemTagViewModel 
    {
       
        public int StatusReportItemTagId { get; set; }
        public int StatusReportItemId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}