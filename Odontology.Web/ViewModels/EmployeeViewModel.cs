using System;

namespace Odontology.Web.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
