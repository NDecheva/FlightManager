using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Data.Entities
{
    public class Passenger : BaseEntity

    {
        public int PassengerId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public int PhoneNumber { get; set; }
        public int Password { get; set; }
        public int PersonalId { get; set; }
        public bool Address { get; set; }
        public bool Role { get; set; }

    }
}
