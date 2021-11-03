using System;
using Microsoft.AspNetCore.Http;
using Odontology.Business.DTO;

namespace Odontology.Web.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public ImageDto Image { get; set; }

        public string Resume { get; set; }

        public IFormFile File { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
