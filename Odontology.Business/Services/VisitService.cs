using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<VisitDto> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<VisitDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void AddOrEdit(VisitDto article)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
