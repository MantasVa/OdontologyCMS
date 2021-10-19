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
            string adminName = Role.Admin.ToDisplayName();
            string doctorName = Role.Doctor.ToDisplayName();
            string userName = Role.User.ToDisplayName();

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole {Id = 1, Name = adminName, NormalizedName = adminName.ToUpper(), CreatedOn = new DateTime(2021,10,19), UpdatedOn = null, CreatedBy = "System", ConcurrencyStamp = "e7873ea9-da5a-40fb-904e-afd86771ef21"},
                new ApplicationRole {Id = 2, Name = doctorName, NormalizedName = doctorName.ToUpper(), CreatedOn = new DateTime(2021,10,19), UpdatedOn = null, CreatedBy = "System", ConcurrencyStamp = "dca9e54e-cdaa-45f1-93f5-6744b69de03c"},
                new ApplicationRole {Id = 3, Name = userName, NormalizedName = userName.ToUpper(), CreatedOn = new DateTime(2021,10,19), UpdatedOn = null, CreatedBy = "System", ConcurrencyStamp = "d77bbed5-82ca-4d49-8484-19b683fb1add"}
            );
        }
    }
}
