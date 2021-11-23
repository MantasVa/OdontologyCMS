using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Odontology.Web.Infrastructure.Attributes;

namespace Odontology.Web.ViewModels
{
    public class VisitCreateViewModel : EntityCreateViewModel<VisitViewModel>
    {
        [DateGreaterThan( "Vizito laikas turėtų būti bent viena kalendorine diena ateityje.")]
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
        
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> UsersSelectEnumerable { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Būtinas daktaro pasirinkimas.")]
        public int EmployeeId { get; set; }

        public IEnumerable<SelectListItem> EmployeesSelectEnumerable { get; set; }
    }
}
