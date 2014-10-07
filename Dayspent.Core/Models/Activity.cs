using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dayspent.Security;

namespace Dayspent.Core.Models
{
    public class Activity : Auditable
    {
        [Key]
        public int ActivityId { get; set; }

        public int TimelineId { get; set; }
        [ForeignKey("TimelineId")]
        public virtual Timeline Timeline { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required, MaxLength]
        public string Description { get; set; }

        [MaxLength(20)]
        public string TimeSpent { get; set; }

        public int? TimeSpentMins { get; set; }

        [Required, MaxLength(130)]
        public string ActivityByUserId { get; set; }

        public virtual ICollection<ActivityTag> Tags { get; set; }
        
    }
}
