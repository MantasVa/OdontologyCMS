﻿using System;

namespace Odontology.Business.DTO
{
    public class VisitDto
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public PatientDto Patient { get; set; }

        public EmployeeDto Doctor { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
