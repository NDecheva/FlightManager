using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Data.Entities
{
    public class Flight : BaseEntity
    {

        public Flight()
        {
            this.Booking = new List<Booking>();
        }

        public int FlightId { get; set; }
        public bool DepartureLocation { get; set; }
        public bool ArrivalLocation { get; set; }
        public int DepartureTime { get; set; }
        public int ArrivalTime { get; set; }
        public bool AircraftType { get; set; }
        public int AircraftId { get; set; }
        public int PersonalId { get; set; }
        public string PilotName { get; set; }
        public string PassengerCapacity { get; set; }
        public string BusinessClassCapacity { get; set; }

        public virtual List<Booking> Booking { get; set; }
    }
}
