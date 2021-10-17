using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Common;
using Odontology.Common.Enums;
using Odontology.Domain.Models;
using Odontology.Web.Infrastructure.Extensions;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AuthorizationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public AuthorizationController(UserManager<ApplicationUser> userManager,
                                       SignInManager<ApplicationUser> signInManager,
                                       IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        public IActionResult Registration() => View();
        
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var createResult = await AddApplicationUserAsync(viewModel);
            if (!createResult.Succeeded && createResult.Errors.Any())
            {
                var error = createResult.Errors.First();
                ModelState.AddModelError(error.Code, error.Description);
                return View(viewModel);
            }


            return RedirectToAction(nameof(Login));
        }

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

        public IActionResult AdminList()
        {
            var usersListViewModel = userService.GetAll().Adapt<IEnumerable<UserViewModel>>();
            return View(usersListViewModel);
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Create() => View(new EntityCreateViewModel<RegistrationViewModel>
                                              {
                                                  EntityViewModel = new RegistrationViewModel(),
                                                  ActionType = ActionTypeEnum.Create
                                              });

        public async Task<IActionResult> Edit(int userId)
        {
            var userDto = await userService.GetByIdAsync(userId);
            var createViewModel = new EntityCreateViewModel<RegistrationViewModel>
            {
                EntityViewModel = userDto.Adapt<RegistrationViewModel>(),
                ActionType = ActionTypeEnum.Edit
            };

            return View(createViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EntityCreateViewModel<RegistrationViewModel> viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.ActionType == ActionTypeEnum.Create)
            {
                var createResult = await AddApplicationUserAsync(viewModel.EntityViewModel.ToApplicationUser(),
                    viewModel.EntityViewModel.Password);
                if (!createResult.Succeeded && createResult.Errors.Any())
                {
                    var error = createResult.Errors.First();
                    ModelState.AddModelError(error.Code, error.Description);
                    return View(viewModel);
                }
            }
            else
            {
                userService.Edit(viewModel.ToUserCreateDto());
            }

            return RedirectToAction(nameof(AdminList));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);

            return RedirectToAction(nameof(AdminList));
        }

        private async Task<IdentityResult> AddApplicationUserAsync(ApplicationUser user, string password)
        {
            var createResult = await userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                _ = await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(user.Email),
                    Role.User.ToDisplayName());
            }
            
            return createResult;
        }
    }
}
