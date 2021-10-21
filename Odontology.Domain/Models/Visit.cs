using System;

namespace Odontology.Domain.Models
{
    public class Visit : BaseEntity
    {
        public DateTime DateTime { get; set; }

        public int PatientId { get; set; }

        public ApplicationUser Patient { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string Comment { get; set; }
    }
}
