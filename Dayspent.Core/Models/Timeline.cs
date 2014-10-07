using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class Timeline : Auditable
    {
        [Key]
        public int TimelineId { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        [Required, MaxLength(130)]
        public string OwnerId { get; set; }


    }
}
