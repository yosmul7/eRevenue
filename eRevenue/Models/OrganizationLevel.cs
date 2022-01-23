using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models
{
    public class OrganizationLevel
    {
        [Key]
        public int OrganizationLevelId { get; set; }
        public string OrganizationLevelNameAmh { get; set; }
        public virtual ICollection<RevenuePlan> RevenuePlan { get; internal set; }
    }
}
