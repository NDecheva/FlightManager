using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.Enums
{
    public enum AircraftType
    {
        [Display(Name = "Passenger Jet")]
        Jet,
        [Display(Name = "Turbo Propeller")]
        Turboprop,
        [Display(Name = "Helicopter")]
        Helicopter
    }
}
