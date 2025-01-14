using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
    public class LoginVM : BaseVM
    {
        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
