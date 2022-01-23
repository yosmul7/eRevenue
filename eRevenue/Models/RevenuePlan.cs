using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models
{
    public class RevenuePlan
    {
        [Key]
        public int RevenuePlanId { get; set; }  
        public int YearId { get; set; }
        public int OrganizationLevelId { get; set; }
        public int? MonthId { get; set; }  

        public int CenterId { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal RevenuePlanAmount { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RevenuePlanDirect { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RevenuePlanIndirect { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal RevenuePlanMunicipality { get; set; }

        public  Center Center { get; set; }
        public  Year Year { get; set; }
        public OrganizationLevel OrganizationLevel { get; set; }
    }
}
