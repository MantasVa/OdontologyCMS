using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odontology.Domain.Models;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthorizationController(UserManager<ApplicationUser> userManager,
                                       SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return Redirect(nameof(HomeController.Index));            
        }
    }
}
