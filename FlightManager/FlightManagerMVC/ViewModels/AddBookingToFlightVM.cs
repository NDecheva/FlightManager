using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagerMVC.ViewModels
{
    public class AddBookingToFlightVM : BaseVM
    {
        public int FlightId { get; set; }
        public int BookingId { get; set; }
        public IEnumerable<SelectListItem> BookingList { get; set; }
    }
}
