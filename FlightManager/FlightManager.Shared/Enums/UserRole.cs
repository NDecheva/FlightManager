using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.Enums
{
    public enum UserRole
    {
        [Display(Name = "User")]
        User = 1,
        [Display(Name = "Admin")]
        Admin = 2
    }
}
