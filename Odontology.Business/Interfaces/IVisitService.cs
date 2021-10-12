using System.Collections.Generic;
using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IVisitService
    {
        Task<VisitDto> GetByIdAsync(int id);

        IEnumerable<VisitDto> GetAll();

        void AddOrEdit(VisitDto article);

        void Delete(int id);
    }
}
