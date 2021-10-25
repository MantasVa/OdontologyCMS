using System;

namespace Odontology.Business.DTO
{
    public class ImageDto
    {
        public int Id { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
