using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayspent.Core.Models
{
    public class Tag : Auditable
    {
        [Key]
        public int TagId { get; set; }
        
        [Required, MaxLength(20)]
        public string Name { get; set; }

        public bool IsPrivate { get; set; }

    }
}
