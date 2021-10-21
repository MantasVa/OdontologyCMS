using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Persistance.Repositories
{
    public class RoleRepository : Repository<ApplicationRole>, IRoleRepository
    {
        private readonly ApplicationDbContext dbContext;
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<string> GetUserRoleNames(int userId)
        {
            return 
                from userRole in dbContext.IdentityUserRoles.Where(x => x.UserId == userId)
                join role in dbContext.ApplicationRoles
                    on userRole.RoleId equals role.Id
                select role.Name;
        }

        public IQueryable<IdentityUserRole<int>> GetUserRolesQuery(int userId)
            => dbContext.IdentityUserRoles.Where(x => x.UserId == userId);
    }
}
