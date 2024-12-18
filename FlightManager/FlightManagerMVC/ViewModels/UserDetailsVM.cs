using FlightManagerMVC.Enums;

namespace FlightManagerMVC.ViewModels
{
    public class UserDetailsVM : BaseVM
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PersonalId { get; set; }
        public string Address { get; set; }
        public UserRole UserRole { get; set; }
        public List<BookingDetailsVM> Bookings { get; set; }
    }
}
