using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Odontology.Business.DTO;
using Odontology.Business.Helpers;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IRepository<Article> articleRepository;
        private readonly IRepository<Image> imageRepository;

        public ArticleService(IRepository<Article> articleRepository,
                              IRepository<Image> imageRepository)
        {
            this.articleRepository = articleRepository;
            this.imageRepository = imageRepository;
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
                    Images = a.Images.Adapt<IEnumerable<ImageDto>>(),
                    CreatedBy = a.CreatedBy,
                    CreatedOn = a.CreatedOn,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedOn = a.UpdatedOn
                });
        }

        public void AddOrEdit(ArticleCreateDto articleCreateDto)
        {
            var article = articleCreateDto.Article.Adapt<Article>();
            if (articleCreateDto.Files?.Count() > 0)
            {
                article.Images = ConvertToArticleImages(articleCreateDto.Files, article.Id);
            }
            
            switch (articleCreateDto.ActionType)
            {
                case ActionTypeEnum.Create:
                    _ = articleRepository.Add(article);
                    break;
                case ActionTypeEnum.Edit:
                    DeleteArticleImages(article.Id);
                    if (article.Images != null)
                    {
                        foreach (var articleImage in article.Images)
                        {
                            imageRepository.Add(articleImage);
                        }
                    }
                    _ = articleRepository.UpdateAsync(article);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task DeleteAsync(int id)
        {
            _ = await articleRepository.DeleteAsync(id);
        }

        private void DeleteArticleImages(int id)
        {
            var articleImages = imageRepository.GetAllQuery().Where(x => x.ArticleId == id).ToList();

            articleImages.ForEach(async x => await imageRepository.DeleteAsync(x.Id));
        }

        private static ICollection<Image> ConvertToArticleImages(IEnumerable<IFormFile> files, int articleId)
        {
            var imagesList = files.ConvertToBytes().Select(x => new Image { ArticleId = articleId, Content = x}).ToList();
            return new Collection<Image>(imagesList);
        }
    }
}
