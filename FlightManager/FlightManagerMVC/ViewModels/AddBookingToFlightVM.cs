using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace FlightManagerMVC.ViewModels
{
    public class AddBookingToFlightVM : BaseVM
    {
        public int FlightId { get; set; }
        public int BookingId { get; set; }

        [DisplayName("Booking")]
        public IEnumerable<SelectListItem> BookingList { get; set; }
    }
}
