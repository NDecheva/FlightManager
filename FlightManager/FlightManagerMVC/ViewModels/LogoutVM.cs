using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
    public class LogoutVM : BaseVM
    {
        [Required]
        public string Message { get; set; }
    }
}
