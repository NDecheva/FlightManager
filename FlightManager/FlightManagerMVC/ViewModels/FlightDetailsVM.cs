using FlightManager.Data.Entities;
using FlightManagerMVC.Enums;
using System.ComponentModel;

namespace FlightManagerMVC.ViewModels
{
    public class FlightDetailsVM : BaseVM
    {
        [DisplayName("Departure Location")]
        public string DepartureLocation { get; set; }

        [DisplayName("Arrival Location")]
        public string ArrivalLocation { get; set; }

        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [DisplayName("Aircraft Type")]
        public AircraftType AircraftType { get; set; }
        public int AircraftId { get; set; }

        [DisplayName("Pilot Name")]
        public string PilotName { get; set; }

        [DisplayName("Passenger Capacity")]
        public int PassengerCapacity { get; set; }

        [DisplayName("Business Class Capacity")]
        public int BusinessClassCapacity { get; set; }
            
        [DisplayName("Bookings")]
        public virtual List<BookingDetailsVM> Bookings { get; set; }
    }
}