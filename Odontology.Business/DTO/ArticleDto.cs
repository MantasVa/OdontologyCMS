using System;
using System.Collections.Generic;

namespace Odontology.Business.DTO
{
    public class ArticleDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public IEnumerable<ImageDto> Images { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
    }
}
