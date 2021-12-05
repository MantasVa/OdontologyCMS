using System;
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
        private readonly int itemsPerPage = 10;

        public ArticleController(IArticleService articleService)
        {
            this.articleService = articleService;
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult AdminList([FromQuery]int pageNumber = 1, [FromQuery]string title = null, [FromQuery]string author = null)
        {
            var articlesViewModel = articleService.GetAll().Adapt<IEnumerable<ArticleViewModel>>();
            
            if (title != null)
            {
                articlesViewModel = articlesViewModel.Where(x => x.Title.ToLower().Contains(title.ToLower()));
            }

            if (author != null)
            {
                articlesViewModel = articlesViewModel.Where(x => x.CreatedBy.ToLower().Contains(author.ToLower()));
            }
            
            var pageArticles = articlesViewModel.OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * itemsPerPage)
                .Take(itemsPerPage).ToList();

            return View(new ArticleAdminIndexViewModel
            {
                Articles = pageArticles,
                PagingInfo = new PagingViewModel
                {
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling((double)articlesViewModel.Count() / itemsPerPage)
                }
            });
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
        public async Task<IActionResult> Create(ArticleCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var articleCreateDto = viewModel.ToArticleCreateDto();

            await articleService.AddOrEditAsync(articleCreateDto);

            return RedirectToAction(nameof(AdminList));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await articleService.DeleteAsync(id);

            return RedirectToAction(nameof(AdminList));
        }
    }
}