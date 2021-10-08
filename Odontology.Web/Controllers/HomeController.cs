using Microsoft.AspNetCore.Mvc;

namespace Odontology.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs() => View();

        public IActionResult Error() => View();
    }
}
