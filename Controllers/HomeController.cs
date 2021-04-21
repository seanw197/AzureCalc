using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using AzureCalc.Models;
using System;

// Sample CA3 - M50 Toll Calculator

namespace AzureCalc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        List<SelectListItem> services = new List<SelectListItem>()
            {
                    new SelectListItem() { Text = "Very Small", Value = "0.02", Selected = true },
                    new SelectListItem() { Text = "Small", Value = "0.08" },
                    new SelectListItem() { Text = "Medium", Value = "0.16" },
                    new SelectListItem() { Text = "Large", Value = "0.32" },
                    new SelectListItem() { Text = "Very Large", Value = "0.64" },
                    new SelectListItem() { Text = "A6", Value = "0.90" },
                    new SelectListItem() { Text = "A7", Value = "1.80" }
            };


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
  
        public IActionResult Calculate()
        {
            Service sv = new Service();
            sv.InstanceList = services;
            return View(sv);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirm(Service sv)
        {
            double Price = sv.PricePerHour * sv.Quantity;
 
            if(DateTime.IsLeapYear(DateTime.Now.Year)) 
            {
                Price *= 366 * 24;
            }
            else
            {
                Price *= 365 * 24;
            }

            // This line uses the returned value from the Calculate view to find the text name of the service
            // in the services list of SelectListItems. The name is then given to the model as Instance.

            sv.Instance = services.Find(s => s.Value == sv.PricePerHour.ToString("0.00")).Text;

            ViewBag.YearPrice = "$" + Price.ToString("0.00");
            return View(sv);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
