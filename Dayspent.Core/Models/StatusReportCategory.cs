using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class StatusReportCategory : Auditable
    {
        [Key]
        public int StatusReportCategoryId { get; set; }

        [Required]
        public int Sequence { get; set; }

        [Required, MaxLength(50)]
        public string Code { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }

        [MaxLength]
        public string Remarks { get; set; }

    }
}
