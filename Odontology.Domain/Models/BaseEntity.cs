using System;
using Odontology.Domain.Interfaces;

namespace Odontology.Domain.Models
{
    public class BaseEntity : IEntity
    {
        public BaseEntity()
        {
            UpdatedOn = DateTime.Now;
            CreatedOn = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
