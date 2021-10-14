using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Odontology.Web.ViewModels
{
    public class VisitCreateViewModel : EntityCreateViewModel<VisitViewModel>
    {
        public int EmployeeId { get; set; }

        public IEnumerable<SelectListItem> EmployeesSelectEnumerable { get; set; }
    }
}
