using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Odontology.Business.DTO;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Common;
using Odontology.Common.Enums;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRoleRepository roleRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(IRepository<ApplicationUser> userRepository, 
                           IRepository<Employee> employeeRepository,
                           IRoleRepository roleRepository,
                           UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.employeeRepository = employeeRepository;
            this.roleRepository = roleRepository;
            this.userManager = userManager;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return
                from user in userRepository.GetAllQuery()
                select new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    CreatedOn = user.CreatedOn,
                    CreatedBy = user.CreatedBy,
                    UpdatedOn = user.UpdatedOn,
                    UpdatedBy = user.UpdatedBy,
                    Roles = roleRepository.GetUserRoleNames(user.Id)
                };
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            return user.Adapt<UserDto>();
        }

        public async Task<IdentityResult> AddUserAsync(ApplicationUser user, string password, string[] roles)
        {
            var createResult = await userManager.CreateAsync(user, password);
            if (createResult.Succeeded)
            {
                _ = await userManager.AddToRolesAsync(await userManager.FindByEmailAsync(user.Email), roles);
            }

            if (roles.Contains(Role.Doctor.ToDisplayName()))
            {
                AddEmployee(await userManager.FindByEmailAsync(user.Email));
            }
            
            return createResult;
        }

        public void Edit(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
                throw new ArgumentNullException(nameof(userCreateDto));
            
            var user = userCreateDto.User.Adapt<ApplicationUser>();

            switch (userCreateDto.ActionType)
            {
                case ActionTypeEnum.Edit:
                    userRepository.UpdateAsync(user);
                    break;
                default:
                    throw new ArgumentException("Action type is not valid", nameof(userCreateDto));
            }
        }

        public async Task<IdentityResult> UpdateUserRolesAsync(string userId, string[] newRoles)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User with this Id does not exist");
            }
            var previousRoles = await userManager.GetRolesAsync(user);
            _ = await userManager.RemoveFromRolesAsync(user, previousRoles);
            var addRoles = await userManager.AddToRolesAsync(user, newRoles);

            var doctorRoleName = Role.Doctor.ToDisplayName();
            if (!previousRoles.Contains(doctorRoleName) && newRoles.Contains(doctorRoleName))
            {
                AddEmployee(user);
            }
            else if (previousRoles.Contains(doctorRoleName) && !newRoles.Contains(doctorRoleName))
            {
                RemoveEmployee(user);
            }

            return addRoles;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user.EmployeeId != null)
            {
                _ = employeeRepository.DeleteAsync((int)user.EmployeeId);
            }
            _ = await userManager.DeleteAsync(user);
        }

        private void AddEmployee(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var employee = employeeRepository.Add(new Employee {CreatedOn = DateTime.UtcNow});
            user.EmployeeId = employee.Id;
            _ = userRepository.UpdateAsync(user);
        }

        private void RemoveEmployee(ApplicationUser user)
        {
            if (user?.EmployeeId == null)
            {
                throw new ArgumentException($"User with id {user.Id} does not have an employee to remove");
            }

            _ = employeeRepository.DeleteAsync((int) user.EmployeeId);
            user.EmployeeId = null;
            _ = userRepository.UpdateAsync(user);
        }


    }
}