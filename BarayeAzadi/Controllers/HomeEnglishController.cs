using BarayeAzadi.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BarayeAzadi.Controllers
{
    public class HomeEnglishController : Controller
    {
        private readonly ILogger<HomeEnglishController> _logger;

        public HomeEnglishController(ILogger<HomeEnglishController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CharterText()
        {
            return View();
        }
        public IActionResult Statements()
        {
            return View();
        }

        public IActionResult Contactus()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
