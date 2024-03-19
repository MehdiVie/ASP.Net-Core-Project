using Microsoft.AspNetCore.Mvc;

namespace BarayeAzadi.Web.Controllers
{
    public class ContactusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateContactus()
        {
            return View();
        }
    }
}
