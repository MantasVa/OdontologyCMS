using System.Collections.Generic;

namespace Odontology.Domain.Models
{
    public class Employee : BaseEntity
    {
        public ICollection<Visit> Visits { get; set; }
    }
}
