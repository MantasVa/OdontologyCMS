using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Web.Infrastructure;
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

        [HttpGet]
        public IActionResult AdminList()
        {
            var articlesViewModel = articleService.GetAll().Adapt<IEnumerable<ArticleViewModel>>();

            return View(articlesViewModel);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var articleDto = await articleService.GetByIdAsync(id);
            var article = articleDto.Adapt<ArticleViewModel>();

            return View(article);
        }

        [HttpGet]
        public IActionResult Create() => View(new ArticleCreateViewModel
        {
            Article = new ArticleViewModel(),
            ViewType = ViewTypeEnum.Create
        });

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var articleDto = await articleService.GetByIdAsync(id);
            var article = articleDto.Adapt<ArticleViewModel>();

            return View(nameof(Create), new ArticleCreateViewModel
            {
                Article = article,
                ViewType = ViewTypeEnum.Edit
            });
        }

        [HttpPost]
        public IActionResult Create(ArticleCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            articleService.AddOrEdit(viewModel.Article.Adapt<ArticleDto>());

            return RedirectToAction(nameof(AdminList));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            articleService.Delete(id);

            return RedirectToAction(nameof(AdminList));
        }
    }
}
