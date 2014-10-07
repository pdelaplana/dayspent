using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class ActivityTagViewModel
    {
       
        public int ActivityTagId { get; set; }
        public int ActivityId { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
    }
}