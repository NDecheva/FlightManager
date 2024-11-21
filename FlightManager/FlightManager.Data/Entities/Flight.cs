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
            this.Bookings = new List<Booking>();
        }

        
        public string DepartureLocation { get; set; }
        public string ArrivalLocation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string AircraftType { get; set; }
        public int AircraftId { get; set; }
        public string PersonalId { get; set; }
        public string PilotName { get; set; }
        public int PassengerCapacity { get; set; }
        public int BusinessClassCapacity { get; set; }

        public virtual List<Booking> Bookings { get; set; }
    }
}
