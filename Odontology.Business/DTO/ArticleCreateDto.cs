using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Odontology.Business.Infrastructure.Enums;

namespace Odontology.Business.DTO
{
    public class ArticleCreateDto
    {
        public ArticleDto Article { get; set; }

        public ActionTypeEnum ActionType { get; set; }

        public IEnumerable<IFormFile> Files { get; set; }
    }
}
