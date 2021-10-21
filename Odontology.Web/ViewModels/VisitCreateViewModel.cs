using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Odontology.Web.ViewModels
{
    public class VisitCreateViewModel : EntityCreateViewModel<VisitViewModel>
    {
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> UsersSelectEnumerable { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Būtinas daktaro pasirinkimas.")]
        public int EmployeeId { get; set; }

        public IEnumerable<SelectListItem> EmployeesSelectEnumerable { get; set; }
    }
}
