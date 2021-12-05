using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly int itemsPerPage = 10;

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

        [Authorize(Roles = "Admin")]
        public IActionResult AdminList(int pageNumber = 1, string name = null, string email = null)
        {
            var employeesViewModel = employeeService.GetAll().Adapt<IEnumerable<EmployeeTableViewModel>>();

            if (name != null)
            {
                employeesViewModel = employeesViewModel.Where(x => $"{x.Name} {x.Surname}".ToLower().Contains(name.ToLower()));
            }

            if (email != null)
            {
                employeesViewModel = employeesViewModel.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }
            
            var pageEmployees = employeesViewModel.OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage).ToList();

            
            return View(new EmployeeAdminIndexViewModel
            {
                Employees = pageEmployees,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)employeesViewModel.Count() / itemsPerPage)
                }
            });
        }
        
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
