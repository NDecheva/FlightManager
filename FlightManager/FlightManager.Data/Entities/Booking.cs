using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Data.Entities
{
    public class Booking : BaseEntity
    {

        public int FlightId { get; set; }
        public int PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Nationality { get; set; }
        public string SeatClass { get; set; }
        public virtual List<Flight> Flights { get; set; }

    }
}
