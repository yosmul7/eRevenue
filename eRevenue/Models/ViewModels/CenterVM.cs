using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRevenue.Models.ViewModels
{
    public class CenterVM
    {
        public Center Center { get; set; }
        [Required]
        public IEnumerable<SelectListItem> TypeDropDownOrginizationLevel { get; set; }

    }
}
