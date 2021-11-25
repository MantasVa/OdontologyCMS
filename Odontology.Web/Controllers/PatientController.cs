using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;

namespace Odontology.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IVisitService visitService;
        private readonly UserManager<ApplicationUser> userManager;
        
        public PatientController(IVisitService visitService,
                                 UserManager<ApplicationUser> userManager)
        {
            this.visitService = visitService;
            this.userManager = userManager;
        }
        
        [Authorize(Roles = "Doctor")]
        public IActionResult Index()
        {
            var currentUserIdString = userManager.GetUserId(User);
            var userId = Convert.ToInt32(currentUserIdString);
            var doctorVisits = visitService.GetByEmployeeId(userId);
            
            return View(doctorVisits);
        }
    }
}