namespace Odontology.Business.DTO
{
    public class UserNameDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Fullname => $"{Name} {Surname}";
    }
}
