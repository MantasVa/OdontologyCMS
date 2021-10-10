using System.Threading.Tasks;
using Mapster;
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
        public IActionResult Registration() => View();
        

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var mapConfig = TypeAdapterConfig<RegistrationViewModel, ApplicationUser>
                            .NewConfig()
                            .Map(dest => dest.UserName,
                                 src => src.Email).Config;

            var user = viewModel.Adapt<ApplicationUser>(mapConfig);
            var result = await userManager.CreateAsync(user, viewModel.Password);

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var result = await signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, false);
            
            if (!result.Succeeded)
            {
                ModelState.TryAddModelError("BadLoginInfo", "Neteisingai įvesti prisijungimo duomenys!");

                return View(viewModel);
            }

            return Redirect("/Home");            
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await signInManager.SignOutAsync();

            return returnUrl != null ? LocalRedirect(returnUrl) : RedirectToPage(returnUrl);
        } 
    }
}
