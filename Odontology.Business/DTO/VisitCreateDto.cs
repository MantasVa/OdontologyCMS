using Odontology.Business.Infrastructure.Enums;

namespace Odontology.Business.DTO
{
    public class VisitCreateDto
    {
        public VisitDto Visit { get; set; }

        public ActionTypeEnum ActionType { get; set; }

        public int EmployeeId { get; set; }

        public int PatientId { get; set; }
    }
}
