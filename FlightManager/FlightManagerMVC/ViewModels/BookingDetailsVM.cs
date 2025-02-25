using FlightManagerMVC.Enums;
using System.ComponentModel;

namespace FlightManagerMVC.ViewModels
{
    public class BookingDetailsVM : BaseVM
    {
        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Nationality")]
        public string Nationality { get; set; }

        [DisplayName("Seat Class")]
        public SeatClass SeatClass { get; set; }

        [DisplayName("Flight ID")]
        public int FlightId { get; set; }

        [DisplayName("Flight")]
        public FlightDetailsVM Flight { get; set; }

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("User")]
        public UserDetailsVM User { get; set; }
    }
}
