using FlightManager.Data.Entities;
using FlightManagerMVC.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
    public class BookingEditVM : BaseVM
    {
        [Required]
        [DisplayName("Personal ID")]
        public string PersonalId { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Nationality")]
        public string Nationality { get; set; }

        [Required]
        [DisplayName("Seat Class")]
        public SeatClass SeatClass { get; set; }
        public IEnumerable<SelectListItem> SeatClassesList { get; set; }

        [Required]
        [DisplayName("Flight")]
        public int FlightId { get; set; }
        public IEnumerable<SelectListItem> FlightsList { get; set; }
    }
}
