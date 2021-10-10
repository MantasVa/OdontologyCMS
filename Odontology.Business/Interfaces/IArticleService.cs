using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    interface IArticleService
    {
        Task<ArticleDto> GetByIdAsync(int id);

        IEnumerable<ArticleDto> GetAllQuery();

        void AddAsync(ArticleDto article);

        void UpdateAsync(ArticleDto article);

        void DeleteAsync(int id);
    }
}
