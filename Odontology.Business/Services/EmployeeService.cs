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
                    Fullname = FormatFullName(user.Name, user.Surname)
                };
            return x;
        }

        private static string FormatFullName(string name, string surname) => $"{name} {surname}";
    }
}
