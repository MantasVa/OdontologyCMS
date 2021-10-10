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
        private readonly IGenericRepository<Article> articleRepository;

        public ArticleService(IGenericRepository<Article> articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public async Task<ArticleDto> GetByIdAsync(int id)
        {
            var article = await articleRepository.GetByIdAsync(id);
            return article.Adapt<ArticleDto>();
        }


        public IEnumerable<ArticleDto> GetAllQuery()
        {
            return articleRepository.GetAllQuery().Select(a =>
                new ArticleDto
                {
                    Title = a.Title,
                    Body = a.Body
                });
        }

        public void AddAsync(ArticleDto article)
        {
            _ = articleRepository.AddAsync(article.Adapt<Article>());
        }

        public void UpdateAsync(ArticleDto article)
        {
            _ = articleRepository.UpdateAsync(article.Adapt<Article>());
        }

        public void DeleteAsync(int id)
        {
            _ = articleRepository.DeleteAsync(id);
        }
    }
}
