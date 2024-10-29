using FakeFB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace FakeFB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor for HomeController
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action method for the Home/Index page
        public IActionResult Index()
        {
            return View();
        }

        // Action method for the Privacy page
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
