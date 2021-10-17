using System.ComponentModel.DataAnnotations;

namespace Odontology.Common.Enums
{
    public enum Role
    {
        [Display(Name = "Admin")]
        Admin = 0,
        [Display(Name = "Doctor")]
        Doctor = 1,
        [Display(Name = "User")]
        User = 2
    }
}
