using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class ActivityTag : Auditable
    {
        [Key]
        public int ActivityTagId { get; set; }

        public int ActivityId { get; set; }
        [ForeignKey("ActivityId ")]
        public virtual Activity Activity { get; set;}

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
    }
}
