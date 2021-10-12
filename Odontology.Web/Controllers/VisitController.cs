using Microsoft.AspNetCore.Mvc;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class VisitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
