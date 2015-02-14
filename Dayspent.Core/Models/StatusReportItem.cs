using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class StatusReportItem : Auditable
    {
        [Key]
        public int StatusReportItemId { get; set; }

        public int StatusReportId { get; set; }
        [ForeignKey("StatusReportId")]
        public virtual StatusReport StatusReport{ get; set;}

        public int StatusReportCategoryId { get; set; }
        [ForeignKey("StatusReportCategoryId")]
        public virtual StatusReportCategory StatusReportCategory { get; set; }

        public int Sequence { get; set; }

        [Required, MaxLength]
        public string Description { get; set; }

        public int? TimeSpentInSecs { get; set; }

        public bool HasRedFlag { get; set; }

        public int? TrackableId { get; set; }
        [ForeignKey("TrackableId")]
        public virtual Trackable Trackable { get; set; }

        public virtual ICollection<StatusReportItemTag> Tags { get; set; }

    }
}
