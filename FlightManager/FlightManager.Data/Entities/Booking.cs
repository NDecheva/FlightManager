using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Data.Entities
{
    public class Booking : BaseEntity
    {
        public int PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PhoneNumber { get; set; }
        public int Password { get; set; }
        public bool Nationality { get; set; }
        public bool SeatClass { get; set; }
    }
}
