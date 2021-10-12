using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class VisitController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
