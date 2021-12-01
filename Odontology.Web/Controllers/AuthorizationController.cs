using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Authorization;
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
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public AuthorizationController(SignInManager<ApplicationUser> signInManager,
                                       UserManager<ApplicationUser> userManager,
                                       IUserService userService,
                                       IRoleService roleService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.userService = userService;
            this.roleService = roleService;
        }

        public IActionResult Registration() => View(new RegistrationViewModel());
        
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var createResult = await userService.AddUserAsync(viewModel.ToApplicationUser(), viewModel.Password, new []{Role.User.ToDisplayName()});
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
        [AllowAnonymous]
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
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await signInManager.SignOutAsync();

            return returnUrl != null ? LocalRedirect(returnUrl) : RedirectToPage(returnUrl);
        } 
        
        [Authorize(Roles = "Admin")]
        public IActionResult AdminList(string name = null, string email = null)
        {
            var usersListViewModel = userService.GetAll().Adapt<IEnumerable<UserViewModel>>();
            
            if (name != null)
            {
                usersListViewModel = usersListViewModel.Where(x => $"{x.Name} {x.Surname}".ToLower().Contains(name.ToLower()));
            }

            if (email != null)
            {
                usersListViewModel = usersListViewModel.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }
            
            return View(usersListViewModel);
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new UserCreateViewModel
                                              {
                                                  EntityViewModel = new RegistrationViewModel(),
                                                  ActionType = ActionTypeEnum.Create,
                                                  Roles = roleService.GetAll().ToSelectListItemsEnumerable()
                                              });

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var registrationViewModel = viewModel.EntityViewModel;
            var createResult = await userService.AddUserAsync(registrationViewModel.ToApplicationUser(), registrationViewModel.Password, viewModel.RoleNames);
            if (!createResult.Succeeded && createResult.Errors.Any())
            {
                var error = createResult.Errors.First();
                ModelState.AddModelError(error.Code, error.Description);    
                return View(viewModel);
            }

            return RedirectToAction(nameof(AdminList));
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult EditRoles(int id) => View(new ChangeRolesViewModel
        {
            UserId = id.ToString(),
            Roles = roleService.GetAll().ToSelectListItemsEnumerable()
        });

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRoles(ChangeRolesViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var addRoles = await userService.UpdateUserRolesAsync(viewModel.UserId, viewModel.RoleNames);

            var user = await userManager.GetUserAsync(User);
            await signInManager.SignInAsync(user, false);
            if (addRoles.Succeeded)
            {
                return RedirectToAction(nameof(AdminList));
            }
            else
            {
                var error = addRoles.Errors.First();
                ModelState.AddModelError(error.Code, error.Description);    
                return View(viewModel);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.DeleteAsync(id);
            return RedirectToAction(nameof(AdminList));
        }
    }
}
