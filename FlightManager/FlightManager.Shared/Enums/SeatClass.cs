using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.Enums
{
    public enum SeatClass
    {
        [Display(Name = "Business Class")]
        BusinessClass,
        [Display(Name = "Economy Class")]
        EconomyClass
    }
}
