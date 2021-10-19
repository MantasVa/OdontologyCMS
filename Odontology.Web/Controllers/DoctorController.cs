using Microsoft.AspNetCore.Mvc;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DoctorController : Controller
    {

        public DoctorController()
        {
            
        }

        public IActionResult AdminList() => View();
    }
}
