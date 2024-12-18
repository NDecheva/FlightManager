using FlightManager.Data.Entities;
using FlightManagerMVC.Enums;
using System.ComponentModel;

namespace FlightManagerMVC.ViewModels
{
    public class FlightDetailsVM : BaseVM
    {
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }

        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Aircraft Type")]
        public AircraftType AircraftType { get; set; }
        public int AircraftId { get; set; }
        public string PilotName { get; set; }
        public int PassengerCapacity { get; set; }
        public int BusinessClassCapacity { get; set; }
            
        [DisplayName("Bookings")]
        public virtual List<BookingDetailsVM> Bookings { get; set; }
    }
}