using FlightManagerMVC.Enums;
using System.ComponentModel;

namespace FlightManagerMVC.ViewModels
{
    public class UserDetailsVM : BaseVM
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Role")]
        public UserRole UserRole { get; set; }

        [DisplayName("Booking")]
        public List<BookingDetailsVM> Bookings { get; set; }
    }
}
