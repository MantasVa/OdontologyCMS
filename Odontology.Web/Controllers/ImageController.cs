using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Odontology.Business.Interfaces;

namespace Odontology.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Render(int id)
        {
            var image = await imageService.GetByIdAsync(id);

            return image == null ? NotFound() : File(image.Content, "image/png");
        }
    }
}
