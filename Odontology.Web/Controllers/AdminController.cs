using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Interfaces;
using Odontology.Web.ViewModels;

namespace Odontology.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IUserService userService;
        private readonly IArticleService articleService;
        private readonly IVisitService visitService;
        
        public AdminController(IEmployeeService employeeService, IUserService userService,
                               IArticleService articleService, IVisitService visitService)
        {
            this.employeeService = employeeService;
            this.userService = userService;
            this.articleService = articleService;
            this.visitService = visitService;
        }

        public IActionResult Index()
        {
            return View(new AdminIndexViewModel
            {
                EmployeeCount = employeeService.GetAll().Count(),
                UserCount = userService.GetAll().Count(),
                ArticleCount = articleService.GetAll().Count(),
                VisitCount = visitService.GetAll().Count()
            });
        }
    }
}
