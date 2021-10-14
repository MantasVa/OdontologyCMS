using System;
using System.Collections.Generic;
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

        public UserService(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAll() 
            => userRepository.GetAllQuery().Adapt<IEnumerable<UserDto>>();

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var visit = await userRepository.GetByIdAsync(id);
            return visit.Adapt<UserDto>();
        }

        public void AddOrEdit(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
                throw new ArgumentNullException(nameof(userCreateDto));
            
            var user = userCreateDto.User.Adapt<ApplicationUser>();

            switch (userCreateDto.ActionType)
            {
                case ActionTypeEnum.Create:
                    userRepository.Add(user);
                    break;
                case ActionTypeEnum.Edit:
                    userRepository.UpdateAsync(user);
                    break;
                default:
                    throw new ArgumentException("Action type is not valid", nameof(userCreateDto));

            }
        }

        public void Delete(int id)
        {
            _ = userRepository.DeleteAsync(id);
        }
    }
}
