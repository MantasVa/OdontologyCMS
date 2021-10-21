using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Odontology.Domain.Models;

namespace Odontology.Persistance.Interfaces
{
    public interface IRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<string> GetUserRoleNames(int userId);

        IQueryable<IdentityUserRole<int>> GetUserRolesQuery(int userId);
    }
}
