using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models
{
    public class Center
    {
        [Key]
        public int CenterId { get; set; }
        [Required]
        public string CenterNameAmh { get; set; }
        [DisplayName("Center Name")]
        public int OrganizationLevelId { get; set; }
        [ForeignKey("OrganizationLevelId")]
        public virtual OrganizationLevel OrganizationLevel { get; set; }
        public virtual ICollection<RevenuePlan> RevenuePlan { get; set; }
    }
}
