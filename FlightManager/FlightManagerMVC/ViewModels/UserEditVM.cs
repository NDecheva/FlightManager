using FlightManagerMVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
    public class UserEditVM : BaseVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PersonalId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [DisplayName("Role")]
        public UserRole Role { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        [Required]
        [DisplayName("Bookings")]
        public int BookingId { get; set; }
        public IEnumerable<SelectListItem> BookingsList { get; set; }
    }
}
