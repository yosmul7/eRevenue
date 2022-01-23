using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models.ViewModels
{
    public class RevenuePlanVM
    {
        public RevenuePlan RevenuePlan { get; set; }
        [Required]
        public IEnumerable<SelectListItem> TypeDropDownYear { get; set; }
        [Required]
        public IEnumerable<SelectListItem> TypeDropDownMonth { get; set; }
        [Required]
        public IEnumerable<SelectListItem> TypeDropDownOrginizationLevel { get; set; }
        [Required]
        public IEnumerable<SelectListItem> TypeDropDownCenter { get; set; }
        [Required]
        public IEnumerable<SelectListItem> TypeDropDownRevenueType { get; set; }

    }
}
