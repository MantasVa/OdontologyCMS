using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Odontology.Domain.Models;

namespace Odontology.Persistance.Interfaces
{
    public interface IRoleRepository : IRepository<ApplicationRole>
    {
        public IEnumerable<string> GetUserRoleNames(int userId);
    }
}
