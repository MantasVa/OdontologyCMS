using Microsoft.AspNetCore.Mvc;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AdminController : Controller
    {
        public AdminController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
