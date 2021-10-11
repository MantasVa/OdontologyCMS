using System;

namespace Odontology.Domain.Models
{
    public class Visit : BaseEntity
    {
        public DateTime DateTime { get; set; }

        public ApplicationUser Doctor { get; set; }
    }
}
