using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Infrastructure.Enums;
using Odontology.Business.Infrastructure.Extensions;
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
            var userEmployee = userRepository.GetAllQuery().FirstOrDefault(x => x.EmployeeId == visit.EmployeeId);

            return visit.ToVisitDto(userEmployee);
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
                    Employee = new EmployeeDto
                    {
                        Id = userEmployee.Id,
                        Name = userEmployee.Name,
                        Surname = userEmployee.Surname
                    },
                    Patient = new UserNameDto
                    {
                        Id = userPatient.Id,
                        Name = userPatient.Name,
                        Surname = userPatient.Surname
                    },
                    CreatedBy = visit.CreatedBy,
                    CreatedOn = visit.CreatedOn,
                    UpdatedBy = visit.UpdatedBy,
                    UpdatedOn = visit.UpdatedOn
                };
        }

        public IEnumerable<VisitDto> GetAll()
        {
            var visitDtoList= 
                visitRepository.GetAllQuery().Select(v =>
                new VisitDto
                {
                    Id = v.Id,
                    DateTime = v.DateTime,
                    Employee = userRepository.GetAllQuery().FirstOrDefault(x => x.EmployeeId == v.EmployeeId).Adapt<EmployeeDto>(),
                    Patient = v.Patient.Adapt<UserNameDto>(),
                    CreatedBy = v.CreatedBy,
                    CreatedOn = v.CreatedOn,
                    UpdatedBy = v.UpdatedBy,
                    UpdatedOn = v.UpdatedOn
                }).ToList();

            return visitDtoList;
        }

        public void AddOrEdit(VisitCreateDto visitCreateDto)
        {
            if (visitCreateDto == null)
                throw new ArgumentNullException(nameof(visitCreateDto));

            var visit = visitCreateDto.ToVisit();
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
