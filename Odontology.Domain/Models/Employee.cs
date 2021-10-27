using System.Collections.Generic;

namespace Odontology.Domain.Models
{
    public class Employee : BaseEntity
    {
        public ICollection<Visit> Visits { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }

        public string Resume { get; set; }
    }
}
