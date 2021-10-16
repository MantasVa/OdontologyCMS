using System;
using System.ComponentModel.DataAnnotations;

namespace Odontology.Web.ViewModels
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Neįvestas Vardas")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Neįvesta Pavardė")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Neįvestas El. Paštas")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Neįvestas Slaptažodis")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Slaptažodis turi būti mažiausiai 6 simbolių")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Neįvestas Slaptažodis")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Slaptažodžiai Nesutampa")]
        public string ConfirmPassword { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }
    }
}
