using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public enum TrackableType {
        Objective,
        Project,
        Task
    }

    public class Trackable : Auditable
    {
        [Key]
        public int TrackableId { get; set; }

        [Required,MaxLength(120)]
        public string Description { get; set; }

        public TrackableType TrackableType { get; set; }

        public virtual ICollection<StatusReportItem> StatusReportItems { get; set; }
    }
}
