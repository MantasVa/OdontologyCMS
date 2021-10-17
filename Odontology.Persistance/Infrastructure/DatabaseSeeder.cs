using System;
using Microsoft.EntityFrameworkCore;
using Odontology.Common;
using Odontology.Common.Enums;
using Odontology.Domain.Models;

namespace Odontology.Persistance.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole {Id = 1, Name = Role.Admin.ToDisplayName(), CreatedOn = DateTime.UtcNow, UpdatedOn = null, CreatedBy = "System"},
                new ApplicationRole {Id = 2, Name = Role.Doctor.ToDisplayName(), CreatedOn = DateTime.UtcNow, UpdatedOn = null, CreatedBy = "System"},
                new ApplicationRole {Id = 3, Name = Role.User.ToDisplayName(), CreatedOn = DateTime.UtcNow, UpdatedOn = null, CreatedBy = "System"}
            );
        }
    }
}
