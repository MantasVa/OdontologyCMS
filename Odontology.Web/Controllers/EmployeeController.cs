using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.DTO;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Web.Infrastructure.Extensions;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public IActionResult AdminList()
        {
            var employeesViewModel = employeeService.GetAll().Adapt<IEnumerable<EmployeeTableViewModel>>();
            return View(employeesViewModel);
        }

        [HttpGet]
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
