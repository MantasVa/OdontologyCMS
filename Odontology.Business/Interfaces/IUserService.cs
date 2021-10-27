using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Odontology.Business.DTO;
using Odontology.Domain.Models;

namespace Odontology.Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        Task<UserDto> GetByIdAsync(int id);

        IEnumerable<UserNameDto> GetByRole(params string[] roleNames);

        Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string[] roles);

        Task EditAsync(UserCreateDto userCreateDto);

        Task<IdentityResult> UpdateUserRolesAsync(string userId, string[] newRoles);

        Task DeleteAsync(int id);
    }
}
