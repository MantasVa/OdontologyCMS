using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Web.Infrastructure.Extensions;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            var employees = employeeService.GetAllDetails();
            
            return View(new AboutViewModel
            {
                Employees = employees
            });
        }

        public IActionResult AdminList()
        {
            var employeesViewModel = employeeService.GetAll().Adapt<IEnumerable<EmployeeTableViewModel>>();
            return View(employeesViewModel);
        }

        public IActionResult Edit(int id)
        {
            var employee = employeeService.GetDetailsById(id);

            return View(new EntityCreateViewModel<EmployeeEditViewModel>
            {
                EntityViewModel = employee.Adapt<EmployeeEditViewModel>(),
                ActionType = ActionTypeEnum.Edit
            });
        }

        [HttpPost]
        public IActionResult Edit(EntityCreateViewModel<EmployeeEditViewModel> viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var employeeEditDto = viewModel.ToEmployeeEditDto();

            employeeService.EditAsync(employeeEditDto);

            return RedirectToAction(nameof(AdminList));
        }
    }
}
