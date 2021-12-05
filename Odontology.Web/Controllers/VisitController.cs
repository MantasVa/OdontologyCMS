using System;
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
    public class VisitController : Controller
    {
        private readonly IVisitService visitService;
        private readonly IEmployeeService employeeService;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly int itemsPerPage = 10;

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

        [Authorize(Roles = "Admin")]
        public IActionResult AdminList(int pageNumber = 1, string patient = null, string doctor = null)
        {
            var visitsViewModel = visitService.GetAll().Adapt<IEnumerable<VisitViewModel>>() ??
                                  new List<VisitViewModel>();
            
            if (patient != null)
            {
                visitsViewModel = visitsViewModel.Where(x => x.Patient.Fullname.ToLower().Contains(patient.ToLower()));
            }

            if (doctor != null)
            {
                visitsViewModel = visitsViewModel.Where(x => x.Employee.Fullname.ToLower().Contains(doctor.ToLower()));
            }
            
            var pageVisits = visitsViewModel.OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage).ToList();
            
            return View(new VisitAdminIndexViewModel
            {
                Visits = pageVisits,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)visitsViewModel.Count() / itemsPerPage)
                }
            });
        }

        [Authorize]
        public IActionResult Index()
        {
            var idString = userManager.GetUserId(User);
            var id = Convert.ToInt32(idString);

            var visits = visitService.GetByPatientId(id).Adapt<IEnumerable<VisitViewModel>>();
            return View(visits);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var visitDto = await visitService.GetByIdAsync(id);
            var visit = visitDto.Adapt<VisitViewModel>();

            SetViewBag();
            return View(visit);
        }

        [Authorize(Roles = "Admin,Doctor,User")]
        public IActionResult Create()
        {
            var viewmodel = new VisitCreateViewModel
            {
                EntityViewModel = new VisitViewModel(),
                ActionType = ActionTypeEnum.Create,
                EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable(),
                UsersSelectEnumerable = userService.GetByRole(Role.User.ToDisplayName()).ToSelectListItemsEnumerable()
            };
            
            return View(viewmodel);
        }

       

        [Authorize(Roles = "Admin,Doctor,User")]
        public async Task<IActionResult> Edit(int id)
        {
            var visitDto = await visitService.GetByIdAsync(id);
            var visit = visitDto.Adapt<VisitViewModel>();

            return View(nameof(Create), new VisitCreateViewModel
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
        [Authorize(Roles = "Admin,Doctor,User")]
        public async Task<IActionResult> Create(VisitCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.EmployeesSelectEnumerable = employeeService.GetAll().ToSelectListItemsEnumerable();
                viewModel.UsersSelectEnumerable =
                    userService.GetByRole(Role.User.ToDisplayName()).ToSelectListItemsEnumerable();
                return View(viewModel);
            }

            var isAdmin = User.IsInRole(Role.Admin.ToDisplayName());
            var userIdString = isAdmin ? viewModel.UserId.ToString() : userManager.GetUserId(User);
            var visitDto = viewModel.ToVisitCreateDto(userIdString);

            await visitService.AddOrEditAsync(visitDto);

            return isAdmin ? RedirectToAction(nameof(AdminList)) : RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor,User")]
        public async Task<IActionResult> Delete(int id)
        {
            await visitService.DeleteAsync(id);

            var isAdmin = User.IsInRole(Role.Admin.ToDisplayName());
            return isAdmin ? RedirectToAction(nameof(AdminList)) : RedirectToAction(nameof(Index));
        }

        private void SetViewBag()
        {
            ViewBag.IsAdmin = User.IsInRole(Role.Admin.ToDisplayName());
            ViewBag.IsDoctor = User.IsInRole(Role.Doctor.ToDisplayName());
            ViewBag.IsUser = User.IsInRole(Role.User.ToDisplayName());
        }
    }
}