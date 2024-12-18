using FlightManagerMVC.Enums;
using System.ComponentModel;

namespace FlightManagerMVC.ViewModels
{
    public class BookingDetailsVM : BaseVM
    {
        public string PersonalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }

        [DisplayName("Seat Class")]
        public SeatClass SeatClass { get; set; }

        public int FlightId { get; set; }

        [DisplayName("Flight")]
        public FlightDetailsVM Flight { get; set; }
    }
}
