using System.Collections.Generic;
using System.Linq;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IEnumerable<RoleDto> GetAll()
        {
            return roleRepository.GetAllQuery().Select(r => 
                        new RoleDto
                        {
                            Name = r.Name
                        });
        }
    }
}
