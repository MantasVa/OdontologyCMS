using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> articleRepository;

        public ArticleService(IRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var article = await articleRepository.GetByIdAsync(id);
            return article.Adapt<ArticleDto>();
        }


        public IEnumerable<ArticleDto> GetAll()
        {
            return articleRepository.GetAllQuery().Select(a =>
                new ArticleDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Body = a.Body,
                    CreatedBy = a.CreatedBy,
                    CreatedOn = a.CreatedOn,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedOn = a.UpdatedOn
                });
        }

        public void AddOrEdit(ArticleDto article)
        {
            if (article.Id == 0)
            {
                _ = articleRepository.Add(article.Adapt<Article>());
            }
            else
            {
                _ = articleRepository.UpdateAsync(article.Adapt<Article>());
            }
        }

        public void Delete(int id)
        {
            _ = articleRepository.DeleteAsync(id);
        }
    }
}
