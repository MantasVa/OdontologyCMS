using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Odontology.Web.ViewModels
{
    public class ArticleCreateViewModel : EntityCreateViewModel<ArticleViewModel>
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
