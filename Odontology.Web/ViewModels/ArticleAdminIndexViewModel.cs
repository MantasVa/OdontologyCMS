using System.Collections.Generic;

namespace Odontology.Web.ViewModels
{
    public class ArticleAdminIndexViewModel
    {
        public IEnumerable<ArticleViewModel> Articles { get; set; }

        public PagingViewModel PagingInfo { get; set; }
    }
}