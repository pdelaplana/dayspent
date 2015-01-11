using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class StatusReportItemTag : Auditable
    {
        [Key]
        public int StatusReportItemTagId { get; set; }

        public int StatusReportItemId { get; set; }
        [ForeignKey("StatusReportItemId")]
        public virtual StatusReportItem StatusReportItem { get; set; }

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
