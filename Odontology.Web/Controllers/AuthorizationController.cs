using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
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

            _ = await userManager.CreateAsync(viewModel.ToApplicationUser(), viewModel.Password);

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

        public IActionResult Create() => View(new EntityCreateViewModel<UserViewModel>
                                              {
                                                  EntityViewModel = new UserViewModel(),
                                                  ActionType = ActionTypeEnum.Create
                                              });

        public async Task<IActionResult> Edit(int userId)
        {
            var userDto = await userService.GetByIdAsync(userId);
            var createViewModel = new EntityCreateViewModel<UserViewModel>
            {
                EntityViewModel = userDto.Adapt<UserViewModel>(),
                ActionType = ActionTypeEnum.Edit
            };

            return View(createViewModel);
        }

        [HttpPost]
        public IActionResult Create(EntityCreateViewModel<UserViewModel> viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            throw new NotImplementedException();

            return RedirectToAction(nameof(AdminList));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);

            return RedirectToAction(nameof(AdminList));
        }
    }
}
