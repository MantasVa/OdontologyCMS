using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.DTO;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Web.Infrastructure;
using Odontology.Web.Infrastructure.Extensions;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class VisitController : Controller
    {
        private readonly IVisitService visitService;
        private readonly IEmployeeService employeeService;
        private readonly UserManager<ApplicationUser> userManager;

        public VisitController(IVisitService visitService,
                               IEmployeeService employeeService,
                               UserManager<ApplicationUser> userManager)
        {
            this.visitService = visitService;
            this.employeeService = employeeService;
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

            return View(visit);
        }

        public IActionResult Create() => View(new VisitCreateViewModel
        {
            EntityViewModel = new VisitViewModel(),
            ActionType = ActionTypeEnum.Create,
            EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable()
        });

        public async Task<IActionResult> Edit(int id)
        {
            var visitDto = await visitService.GetByIdAsync(id);
            var visit = visitDto.Adapt<VisitViewModel>();

            return View(nameof(Create),new VisitCreateViewModel
            {
                EntityViewModel = visit,
                ActionType = ActionTypeEnum.Edit,
                EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable()
            });
        }

        [HttpPost]
        public IActionResult Create(VisitCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userIdString = userManager.GetUserId(User);
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
    }
}
