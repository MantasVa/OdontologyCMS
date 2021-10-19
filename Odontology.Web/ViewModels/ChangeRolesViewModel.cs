using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Odontology.Web.ViewModels
{
    public class ChangeRolesViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Turi būti pasirinkta bent viena naudotojo rolė.")]
        public string[] RoleNames { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
