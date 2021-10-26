using System.Collections.Generic;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll();

        IEnumerable<EmployeeDetailedDto> GetAllDetailed();
    }
}
