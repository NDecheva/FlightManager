using AutoMapper;

using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using FlightManager.Shared.Services.Contracts;
using FlightManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace FlightManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin, Employee, User")]
    public class BookingController : BaseCrudController<BookingDto, IBookingRepository, IBookingsService, BookingEditVM ,BookingDetailsVM>

    {
       
        public BookingController(IBookingsService service,IMapper mapper): base(service,mapper)
        {

        }
       
        
                
        

    }
}
