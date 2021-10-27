using System.Collections.Generic;
using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IVisitService
    {
        Task<VisitDto> GetByIdAsync(int id);

        IEnumerable<VisitDto> GetByPatientId(int userId);

        IEnumerable<VisitDto> GetAll();

        Task AddOrEditAsync(VisitCreateDto visitDto);

        Task DeleteAsync(int id);
    }
}
