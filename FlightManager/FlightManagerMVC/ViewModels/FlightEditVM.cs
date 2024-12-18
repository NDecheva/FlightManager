using FlightManager.Data.Entities;
using FlightManagerMVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
    public class FlightEditVM : BaseVM
    {
        [Required]
        [DisplayName("Departure Location")]
        public string DepartureLocation { get; set; }

        [Required]
        [DisplayName("Arrival Location")]
        public string ArrivalLocation { get; set; }

        [Required]
        [DisplayName("Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [DisplayName("Aircraft Type")]
        public AircraftType AircraftType { get; set; }

        [Required]
        public int AircraftId { get; set; }

        [Required]
        public string PilotName { get; set; }

        [Required]
        public int PassengerCapacity { get; set; }

        [Required]
        public int BusinessClassCapacity { get; set; }

        [Required]
        [DisplayName("Bookings")]
        public int BookingId { get; set; }
        public IEnumerable<SelectListItem> Bookings { get; set; }
    }
}
