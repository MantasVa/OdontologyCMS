using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult AdminList()
        {
            var articlesViewModel = articleService.GetAll().Adapt<IEnumerable<ArticleViewModel>>();

            return View(articlesViewModel);
        }

        [Authorize]
        public IActionResult Index()
        {
            var articlesViewModel = 
                 articleService.GetAll()
                .Select(x => x.ToArticleViewModel()).ToList();

            return View(articlesViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var articleDto = await articleService.GetByIdAsync(id);
            var article = articleDto.Adapt<ArticleViewModel>();

            return View(article);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View(new ArticleCreateViewModel
        {
            EntityViewModel = new ArticleViewModel(),
            ActionType = ActionTypeEnum.Create
        });

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var articleDto = await articleService.GetByIdAsync(id);
            var article = articleDto.Adapt<ArticleViewModel>();

            return View(nameof(Create), new ArticleCreateViewModel
            {
                EntityViewModel = article,
                ActionType = ActionTypeEnum.Edit
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ArticleCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var articleCreateDto = viewModel.ToArticleCreateDto();

            articleService.AddOrEdit(articleCreateDto);

            return RedirectToAction(nameof(AdminList));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            articleService.DeleteAsync(id);

            return RedirectToAction(nameof(AdminList));
        }
    }
}