using FlightManagerMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Shared.Dtos
{
    public class BookingDto : BaseModel
    {
        public string PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public string Nationality { get; set; }
        public SeatClass SeatClass { get; set; }

        public int FlightId { get; set; }
        public FlightDto Flight { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
