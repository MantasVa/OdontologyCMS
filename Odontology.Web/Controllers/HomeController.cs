using Microsoft.AspNetCore.Mvc;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        public IActionResult Privacy() => View();

        public IActionResult Error() => View();
    }
}
