using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class StatusReport : Auditable
    {
        [Key]
        public int StatusReportId { get; set; }
        
        [Required]
        public DateTime ReportDate { get; set; }

        [Required,MaxLength(130)]
        public string ReportingUserId { get; set; }

        public DateTime? SubmittedDate { get; set; }

        public string Suggestions { get; set; }

        public virtual ICollection<StatusReportItem> StatusReportItems { get; set; }
    }
}
