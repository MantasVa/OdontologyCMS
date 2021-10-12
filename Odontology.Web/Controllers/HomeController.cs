using Microsoft.AspNetCore.Mvc;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult AboutUs() => View();

        public IActionResult Error() => View();
    }
}
