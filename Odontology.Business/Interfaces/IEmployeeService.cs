using System.Collections.Generic;
using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAll();

        IEnumerable<EmployeeDetailedDto> GetAllDetails();

        EmployeeDetailedDto GetDetailsById(int id);

        Task EditAsync(EmployeeEditDto employeeEditDto);
    }
}
