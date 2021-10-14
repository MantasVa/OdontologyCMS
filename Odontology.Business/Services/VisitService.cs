using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class VisitService : IVisitService
    {
        private readonly IRepository<Visit> visitRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public VisitService(IRepository<Visit> visitRepository,
                            IRepository<ApplicationUser> userRepository)
        {
            this.visitRepository = visitRepository;
            this.userRepository = userRepository;
        }

        public async Task<VisitDto> GetByIdAsync(int id)
        {
            var visit = await visitRepository.GetByIdAsync(id);
            return visit.Adapt<VisitDto>();
        }

        public IEnumerable<VisitDto> GetByPatientId(int patientId)
        {
            return 
                from visit in visitRepository.GetAllQuery()
                join userEmployee in userRepository.GetAllQuery()
                    on visit.EmployeeId equals userEmployee.Id
                join userPatient in userRepository.GetAllQuery()
                    on visit.Patient.Id equals userPatient.Id
                where visit.Patient.Id == patientId
                select new VisitDto
                {
                    Id = visit.Id,
                    Doctor = new EmployeeDto
                    {
                        Id = userEmployee.Id,
                        Fullname = userEmployee.Name,
                    },
                    Patient = new PatientDto
                    {
                        Id = userPatient.Id
                    },
                    CreatedBy = visit.CreatedBy,
                    CreatedOn = visit.CreatedOn,
                    UpdatedBy = visit.UpdatedBy,
                    UpdatedOn = visit.UpdatedOn
                };
        }

        public IEnumerable<VisitDto> GetAll()
        {
            return visitRepository.GetAllQuery().Select(v =>
                new VisitDto
                {
                    Id = v.Id,
                    Doctor = v.Employee.Adapt<EmployeeDto>(),
                    Patient = v.Patient.Adapt<PatientDto>(),
                    CreatedBy = v.CreatedBy,
                    CreatedOn = v.CreatedOn,
                    UpdatedBy = v.UpdatedBy,
                    UpdatedOn = v.UpdatedOn
                });
        }

        public void AddOrEdit(VisitCreateDto visitCreateDto)
        {
            if (visitCreateDto == null)
                throw new ArgumentNullException(nameof(visitCreateDto));
            
            var visit = visitCreateDto.Visit.Adapt<Visit>();
            visit = visitCreateDto.Adapt(visit);

            switch (visitCreateDto.ActionType)
            {
                case ActionTypeEnum.Create:
                    visitRepository.Add(visit);
                    break;
                case ActionTypeEnum.Edit:
                    visitRepository.UpdateAsync(visit);
                    break;
                default:
                    throw new ArgumentException("Action type is not valid", nameof(visitCreateDto));

            }
        }

        public void Delete(int id)
        {
            _ = visitRepository.DeleteAsync(id);
        }
    }
}
