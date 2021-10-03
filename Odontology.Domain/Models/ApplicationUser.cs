﻿using System;
using Microsoft.AspNetCore.Identity;
using Odontology.Domain.Interfaces;

namespace Odontology.Domain.Models
{
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public ApplicationUser()
        {
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
