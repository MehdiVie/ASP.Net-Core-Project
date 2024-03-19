using Microsoft.AspNetCore.Mvc;

namespace BarayeAzadi.Web.Controllers
{
    public class HomeFarsiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
