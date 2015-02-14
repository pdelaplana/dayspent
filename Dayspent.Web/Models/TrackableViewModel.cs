using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class TrackableViewModel
    {
        public int TrackableId { get; set; }
        public string Description { get; set; }
        public string TrackableType { get; set; }
    }
}