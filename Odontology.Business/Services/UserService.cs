using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Employee> employeeRepository;

        public UserService(IRepository<ApplicationUser> userRepository, IRepository<Employee> employeeRepository)
        {
            this.userRepository = userRepository;
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<UserDto> GetAll() 
            => userRepository.GetAllQuery().Adapt<IEnumerable<UserDto>>();

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            return user.Adapt<UserDto>();
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

        public async Task Delete(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user.EmployeeId > 0)
            {
                _ = employeeRepository.DeleteAsync((int)user.EmployeeId);
            }
            _ = userRepository.DeleteAsync(id);
        }
    }
}
