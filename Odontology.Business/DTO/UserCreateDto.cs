using Odontology.Business.Infrastructure.Enums;

namespace Odontology.Business.DTO
{
    public class UserCreateDto
    {
        public UserDto User { get; set; }

        public ActionTypeEnum ActionType { get; set; }


    }
}
