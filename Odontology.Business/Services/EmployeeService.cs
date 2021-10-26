using System.Collections.Generic;
using System.Linq;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public EmployeeService(IRepository<Employee> employeeRepository,
                               IRepository<ApplicationUser> userRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var x =
                from employee in employeeRepository.GetAllQuery()
                join user in userRepository.GetAllQuery()
                    on employee.Id equals user.Employee.Id
                select new EmployeeDto
                {
                    Id = employee.Id,
                    Name = user.Name,
                    Surname = user.Surname
                };
            return x;
        }

        public IEnumerable<EmployeeDetailedDto> GetAllDetailed()
        {
            var x =
                from employee in employeeRepository.GetAllQuery()
                join user in userRepository.GetAllQuery()
                    on employee.Id equals user.Employee.Id
                select new EmployeeDetailedDto
                {
                    Id = employee.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                };
            return x;
        }
    }
}
