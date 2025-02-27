using Microsoft.AspNetCore.Mvc;

namespace BarayeAzadi.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Statements()
        {
            return View();
        }
        public IActionResult CharterText()
        {
            return View();
        }
    }
}
