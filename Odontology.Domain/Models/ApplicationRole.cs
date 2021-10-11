using System;
using Microsoft.AspNetCore.Identity;
using Odontology.Domain.Interfaces;

namespace Odontology.Domain.Models
{
    public class ApplicationRole : IdentityRole<int>, IEntity
    {
        public ApplicationRole()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
