using System.Collections.Generic;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAll();
    }
}
