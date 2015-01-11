using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class ReportingGroup : Auditable
    {
        [Key]
        public int ReportingGroupId { get; set; }
        
        [Required, MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<ReportingGroupMember> ReportingGroupMembers { get; set; }
    }
}
