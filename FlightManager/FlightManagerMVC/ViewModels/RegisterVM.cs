using FlightManager.Data.Entities;
using FlightManagerMVC.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }

        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
    }
}
