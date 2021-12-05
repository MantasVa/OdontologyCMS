using System.Collections.Generic;

namespace Odontology.Web.ViewModels
{
    public class VisitAdminIndexViewModel
    {
        public IEnumerable<VisitViewModel> Visits { get; set; }

        public PagingViewModel PagingInfo { get; set; }
    }
}