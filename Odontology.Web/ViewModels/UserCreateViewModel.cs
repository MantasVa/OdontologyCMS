using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Odontology.Web.ViewModels
{
    public class UserCreateViewModel : EntityCreateViewModel<RegistrationViewModel>
    {
        public string[] RoleNames { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
