using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Helpers;
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

        private readonly int hoursTillVisitToModify = 12;

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
            if (patientId == 0) throw new ArgumentException("User id is incorrect", nameof(patientId));
            
            return 
                from visit in visitRepository.GetAllQuery()
                join userEmployee in userRepository.GetAllQuery()
                    on visit.EmployeeId equals userEmployee.EmployeeId
                join userPatient in userRepository.GetAllQuery()
                    on visit.Patient.Id equals userPatient.Id
                where visit.Patient.Id == patientId
                select new VisitDto
                {
                    Id = visit.Id,
                    DateTime = visit.DateTime,
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

        public IEnumerable<VisitDto> GetByEmployeeId(int employeeUserId)
        {
            if (employeeUserId == 0) throw new ArgumentException("Employee id is incorrect", nameof(employeeUserId));
            
            return 
                from visit in visitRepository.GetAllQuery()
                join userEmployee in userRepository.GetAllQuery()
                    on visit.EmployeeId equals userEmployee.EmployeeId
                join userPatient in userRepository.GetAllQuery()
                    on visit.Patient.Id equals userPatient.Id
                where userEmployee.Id == employeeUserId
                select new VisitDto
                {
                    Id = visit.Id,
                    DateTime = visit.DateTime,
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

        public async Task AddOrEditAsync(VisitCreateDto visitCreateDto)
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
                    await visitRepository.UpdateAsync(visit);
                    break;
                default:
                    throw new ArgumentException("Action type is not valid", nameof(visitCreateDto));
            }
            await SendClientEmailAboutVisitAsync(visitCreateDto.ActionType, visit.PatientId, visit.DateTime);
        }

        /// <summary>
        /// Deletes visit if visit time is at least 12 hours in the future. Past visits or visits less than 12h away can not be deleted from database.
        /// </summary>
        /// <param name="id">Visit id</param>
        /// <returns>DeleteAsync Task</returns>
        public async Task DeleteAsync(int id)
        {
            var visit = await visitRepository.GetByIdAsync(id);

            _ = visit.DateTime > DateTime.UtcNow.AddHours(hoursTillVisitToModify)
                ? await visitRepository.DeleteAsync(id)
                : throw new ArgumentException("Unable to delete visit");
            
            await SendClientEmailAboutVisitAsync(ActionTypeEnum.Delete, visit.PatientId, visit.DateTime);
        }

        private async Task SendClientEmailAboutVisitAsync(ActionTypeEnum actionType, int patientId, DateTime date)
        {
            var patient = await userRepository.GetByIdAsync(patientId);
            
            string subject = "Vizitas Dentmedic";
            string body = "";
            switch (actionType)
            {
                case ActionTypeEnum.Create:
                    body = $"Jums buvo sukurtas naujas vizitas Dentmedic klinikoje. Vizito laikas: {date.ToString("MM/dd/yyyy HH:mm")}";
                    break;
                case ActionTypeEnum.Edit:
                    body = $"Jūsų vizitas Dentmedic klinikoje buvo redaguotas. Vizito laikas: {date.ToString("MM/dd/yyyy HH:mm")}";
                    break;
                case ActionTypeEnum.Delete:
                    body = $"Jūsų vizitas {date.ToString("MM/dd/yyyy HH:mm")}  Dentmedic klinikoje buvo panaikintas.";
                    break;
                default:
                    throw new ArgumentException("Action type is not valid", nameof(actionType));
            }

            EmailHelper.Send(patient.Email, subject, body);
        }
    }
}
