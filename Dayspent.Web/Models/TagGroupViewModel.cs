using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dayspent.Web.Models
{
    public class TagGroupViewModel
    {
        public int TagGroupId { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }

    }
}