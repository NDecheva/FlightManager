using FlightManagerMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Shared.Dtos
{
    public class FlightDto : BaseModel
    {
        public FlightDto()
        {
            this.Bookings = new List<BookingDto>();
        }


        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public AircraftType AircraftType { get; set; }
        public int AircraftId { get; set; }
        public int PersonalId { get; set; }
        public string PilotName { get; set; }
        public int PassengerCapacity { get; set; }
        public int BusinessClassCapacity { get; set; }

        public virtual List<BookingDto> Bookings { get; set; }
    }
}
