using System.Collections.Generic;

namespace Odontology.Web.ViewModels
{
    public class UserAdminIndexViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public PagingViewModel PagingInfo { get; set; }
    }
}