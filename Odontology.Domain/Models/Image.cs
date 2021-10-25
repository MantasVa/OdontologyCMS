namespace Odontology.Domain.Models
{
    public class Image : BaseEntity
    {
        public byte[] Content { get; set; }
        public int? ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
