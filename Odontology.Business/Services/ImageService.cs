using System.Threading.Tasks;
using Mapster;
using Odontology.Business.DTO;
using Odontology.Business.Interfaces;
using Odontology.Domain.Models;
using Odontology.Persistance.Interfaces;

namespace Odontology.Business.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> imageRepository;

        public ImageService(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task<ImageDto> GetByIdAsync(int id) 
            => (await imageRepository.GetByIdAsync(id)).Adapt<ImageDto>();
    }
}
