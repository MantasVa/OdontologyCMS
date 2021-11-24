using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Helpers;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Image> imageRepository;

        public EmployeeService(IRepository<Employee> employeeRepository,
                               IRepository<ApplicationUser> userRepository,
                               IRepository<Image> imageRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees =
                from employee in employeeRepository.GetAllQuery()
                join user in userRepository.GetAllQuery()
                    on employee.Id equals user.EmployeeId
                select new EmployeeDto
                {
                    Id = employee.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                };
            return employees;
        }

        public IEnumerable<EmployeeDetailedDto> GetAllDetails()
        {
            var employees =
                from employee in employeeRepository.GetAllQuery()
                join user in userRepository.GetAllQuery()
                    on employee.Id equals user.Employee.Id
                select new EmployeeDetailedDto
                {
                    Id = employee.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Image = employee.Image.Adapt<ImageDto>(),
                    Resume = employee.Resume,
                    CreatedOn = employee.CreatedOn,
                    UpdatedOn = employee.UpdatedOn,
                    CreatedBy = employee.CreatedBy,
                    UpdatedBy = employee.UpdatedBy
                };

            return employees;
        }

        public EmployeeDetailedDto GetDetailsById(int id)
        {
            var employeeDto =
                (from employee in employeeRepository.GetAllQuery()
                join user in userRepository.GetAllQuery()
                    on employee.Id equals user.Employee.Id
                where employee.Id == id
                select new EmployeeDetailedDto
                {
                    Id = employee.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Image = employee.Image.Adapt<ImageDto>(),
                    Resume = employee.Resume,
                    CreatedOn = employee.CreatedOn,
                    UpdatedOn = employee.UpdatedOn,
                    CreatedBy = employee.CreatedBy,
                    UpdatedBy = employee.UpdatedBy
                }).FirstOrDefault();

            return employeeDto ?? throw new ArgumentException("Bad employee id");
        }

        public async Task EditAsync(EmployeeEditDto employeeEditDto)
        {
            if (employeeEditDto == null)
            {
                throw new ArgumentNullException(nameof(employeeEditDto));
            }

            var employee = employeeEditDto.EmployeeDetailedDto.Adapt<Employee>();

            if (employeeEditDto.File != null)
            {
                if (employee.ImageId.HasValue)
                {
                    _ = await imageRepository.DeleteAsync((int)employee.ImageId);
                }
                var image = imageRepository.Add(new Image {Content = employeeEditDto.File.ConvertToBytes()});
                employee.ImageId = image.Id;
            }

            _ = await employeeRepository.UpdateAsync(employee);
        }
    }
}
