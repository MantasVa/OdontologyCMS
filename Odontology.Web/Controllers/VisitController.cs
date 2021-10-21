using System;
using System.Collections.Generic;
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
    public class VisitController : Controller
    {
        private readonly IVisitService visitService;
        private readonly IEmployeeService employeeService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public VisitController(IVisitService visitService,
                               IEmployeeService employeeService,
                               IUserService userService,
                               UserManager<ApplicationUser> userManager)
        {
            this.visitService = visitService;
            this.employeeService = employeeService;
            this.userService = userService;
            this.userManager = userManager;
        }

        public IActionResult AdminList()
        {
            var visitsViewModel = visitService.GetAll().Adapt<IEnumerable<VisitViewModel>>();
            return View(visitsViewModel);
        }

        public IActionResult Index()
        {
            var idString = userManager.GetUserId(User);
            var id = Convert.ToInt32(idString);
            if (id > 0)
            {
                var visits = visitService.GetByPatientId(id).Adapt<IEnumerable<VisitViewModel>>();
                return View(visits);
            }

            return RedirectToAction("Error", "Home");
        }

        public async Task<IActionResult> Details(int id)
        {
            var visitDto = await visitService.GetByIdAsync(id);
            var visit = visitDto.Adapt<VisitViewModel>();

            SetViewBag();
            return View(visit);
        }

        public IActionResult Create() => View(new VisitCreateViewModel
        {
            EntityViewModel = new VisitViewModel(),
            ActionType = ActionTypeEnum.Create,
            EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable(),
            UsersSelectEnumerable = userService.GetByRole(Role.User.ToDisplayName()).ToSelectListItemsEnumerable()
        });

        public async Task<IActionResult> Edit(int id)
        {
            var visitDto = await visitService.GetByIdAsync(id);
            var visit = visitDto.Adapt<VisitViewModel>();

            return View(nameof(Create),new VisitCreateViewModel
            {
                EntityViewModel = visit,
                ActionType = ActionTypeEnum.Edit,
                EmployeeId = visit.Employee.Id,
                EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable(),
                UserId = visit.Patient.Id,
                UsersSelectEnumerable = userService.GetByRole(Role.User.ToDisplayName()).ToSelectListItemsEnumerable()
            });
        }

        [HttpPost]
        public IActionResult Create(VisitCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable();
                viewModel.UsersSelectEnumerable = userService.GetByRole(Role.User.ToDisplayName()).ToSelectListItemsEnumerable();
                return View(viewModel);
            }

            var userIdString = User.IsInRole(Role.Admin.ToDisplayName())
                                ? viewModel.UserId.ToString()
                                : userManager.GetUserId(User);

            var visitDto = viewModel.ToVisitCreateDto(userIdString);
            visitService.AddOrEdit(visitDto);

            return RedirectToAction(nameof(AdminList));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            visitService.Delete(id);

            return RedirectToAction(nameof(AdminList));
        }

        private void SetViewBag()
        {
            ViewBag.IsAdmin = User.IsInRole(Role.Admin.ToDisplayName());
            ViewBag.IsDoctor = User.IsInRole(Role.Doctor.ToDisplayName());
            ViewBag.IsUser = User.IsInRole(Role.User.ToDisplayName());
        }
    }
}
