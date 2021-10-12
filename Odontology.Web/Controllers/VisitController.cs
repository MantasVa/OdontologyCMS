using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Web.Infrastructure;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class VisitController : Controller
    {
        private readonly IVisitService visitService;
        private readonly UserManager<ApplicationUser> userManager;

        public VisitController(IVisitService visitService)
        {
            this.visitService = visitService;
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

        public IActionResult Create() => View(new EntityCreateViewModel<VisitViewModel>
        {
            EntityViewModel = new VisitViewModel(),
            ViewType = ViewTypeEnum.Create
        });

        public async Task<IActionResult> Edit(int id)
        {
            var visitDto = await visitService.GetByIdAsync(id);
            var visit = visitDto.Adapt<VisitViewModel>();

            return View(nameof(Create),new EntityCreateViewModel<VisitViewModel>
            {
                EntityViewModel = visit,
                ViewType = ViewTypeEnum.Edit
            });
        }

        [HttpPost]
        public IActionResult Create(EntityCreateViewModel<VisitViewModel> viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            visitService.AddOrEdit(viewModel.EntityViewModel.Adapt<VisitDto>());

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
