using System;
using Odontology.Business.DTO;
using Odontology.Web.Infrastructure.Attributes;

namespace Odontology.Web.ViewModels
{
    public class VisitViewModel
    {
        public int Id { get; set; }

        [DateGreaterThan( "Vizito laikas turėtų būti bent viena kalendorine diena ateityje.")]
        public DateTime DateTime { get; set; }

        public string Comment { get; set; }

        public UserNameDto Patient { get; set; }

        public EmployeeDto Employee { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
