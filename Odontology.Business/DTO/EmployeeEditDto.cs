using Microsoft.AspNetCore.Http;
using Odontology.Business.Infrastructure.Enums;

namespace Odontology.Business.DTO
{
    public class EmployeeEditDto
    {
        public EmployeeDetailedDto EmployeeDetailedDto { get; set; }

        public IFormFile File { get; set; }

        public ActionTypeEnum ActionType { get; set; }
    }
}
