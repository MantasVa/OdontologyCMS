using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class VisitService : IVisitService
    {
        private readonly IRepository<Visit> visitRepository;
        public VisitService(IRepository<Visit> visitRepository)
        {
            this.visitRepository = visitRepository;
        }

        public async Task<VisitDto> GetByIdAsync(int id)
        {
            var visit = await visitRepository.GetByIdAsync(id);
            return visit.Adapt<VisitDto>();
        }

        public IEnumerable<VisitDto> GetByPatientId(int patientId)
        {
            return visitRepository.GetAllQuery().Where(x => x.Patient.Id == patientId).Select(v =>
                new VisitDto
                {
                    Id = v.Id
                });
        }

        public IEnumerable<VisitDto> GetAll()
        {
            return visitRepository.GetAllQuery().Select(v =>
                new VisitDto
                {
                    Id = v.Id
                });
        }

        public void AddOrEdit(VisitDto visitDto)
        {
            if (visitDto.Id == 0)
            {
                _ = visitRepository.Add(visitDto.Adapt<Visit>());
            }
            else
            {
                _ = visitRepository.UpdateAsync(visitDto.Adapt<Visit>());
            }
        }

        public void Delete(int id)
        {
            _ = visitRepository.DeleteAsync(id);
        }
    }
}
