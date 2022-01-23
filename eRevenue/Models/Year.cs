using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models
{
    public class Year
    {
        [Key]
        public int YearId { get; set; }
        [Required]
        public string YearName { get; set; }
        public virtual ICollection<RevenuePlan> RevenuePlan { get; internal set; }

    }
}
