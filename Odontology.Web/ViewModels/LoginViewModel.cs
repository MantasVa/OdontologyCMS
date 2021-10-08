using System.ComponentModel.DataAnnotations;

namespace Odontology.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Neįvestas El. Paštas")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Neįvestas Slaptažodis")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
