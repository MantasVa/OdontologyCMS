﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDto> GetByIdAsync(int id);

        IEnumerable<ArticleDto> GetAll();

        void AddOrEdit(ArticleDto article);

        void Delete(int id);
    }
}
