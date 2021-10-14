using System.Collections.Generic;
using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        Task<UserDto> GetByIdAsync(int id);

        void AddOrEdit(UserCreateDto userCreateDto);

        void Delete(int id);
    }
}
