using System.Collections.Generic;

namespace Odontology.Domain.Models
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
