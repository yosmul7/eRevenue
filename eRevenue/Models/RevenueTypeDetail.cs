using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models
{
    public class RevenueTypeDetail
    {
        [Key]
        public int RevenueTypeDetailId { get; set; }
        [Required]
        public string RevenueCode { get; set; }
        [Required]
        public string RevenueTypeDetailName { get; set; }
        [Required]
        public string RevenueType { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public int RevenuePlanId { get; set; }
        public decimal RevenuePlan { get; set; }
    }
}
