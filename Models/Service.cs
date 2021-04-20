using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureCalc.Models
{
    public class Service 
    {
        public string Instance { get; set; }

        [Required]
        [Range(2, 100000)]
        public int Quantity { get; set; }

        public double PricePerHour { get; set; }
        public IEnumerable<SelectListItem> InstanceList { get; set; }
    } 
}
