using System.ComponentModel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightManagerMVC.ViewModels
{
        public class ChangePasswordVM : BaseVM
    {
            [Required]
            public string Username { get; set; }
            [Required]

            public string Password { get; set; }

            [Required]
            [DisplayName("New Password")]
            public string NewPassword { get; set; }
        }
}
