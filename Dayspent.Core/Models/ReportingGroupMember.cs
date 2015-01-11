using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class ReportingGroupMember : Auditable
    {
        [Key]
        public int ReportingGroupMemberId { get; set; }

        public int ReportingGroupId { get; set; }
        [ForeignKey("ReportingGroupId")]
        public virtual ReportingGroup ReportingGroup { get; set; }

        [Required, MaxLength(130)]
        public string MemberUserId { get; set; }
    }
}
