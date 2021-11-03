using System.Collections.Generic;
using Odontology.Business.DTO;

namespace Odontology.Web.ViewModels
{
    public class AboutViewModel
    {
        public IEnumerable<EmployeeDetailedDto> Employees { get; set; }
    }
}