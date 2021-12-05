using System.Collections.Generic;

namespace Odontology.Web.ViewModels
{
    public class EmployeeAdminIndexViewModel
    {
        public IEnumerable<EmployeeTableViewModel> Employees { get; set; }

        public PagingViewModel PagingInfo { get; set; }
    }
}