using System.Collections.Generic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Interfaces;
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
            var employeesViewModel = employeeService.GetAllDetailed().Adapt<IEnumerable<EmployeeViewModel>>();
            return View(employeesViewModel);
        }
    }
}
