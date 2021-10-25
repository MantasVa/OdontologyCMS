using System.Threading.Tasks;
using Odontology.Business.DTO;

namespace Odontology.Business.Interfaces
{
    public interface IImageService
    {
        Task<ImageDto> GetByIdAsync(int id);
    }
}
