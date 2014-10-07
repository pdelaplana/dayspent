using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Dayspent.Security;

namespace Dayspent.Core.Models
{
    public class Auditable
    {
        [Required]
        public int TenantId { get; set; }

        [Required]
        [StringLength(130)]
        public string CreatedByUserId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(130)]
        public string ModifiedByUserId { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }


    }
}
