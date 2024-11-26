using FlightManagerMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Data.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PersonalId { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
