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
    public class FlightController : BaseCrudController<FlightDto, IFlightRepository, IFlightsService, FlightEditVM, FlightDetailsVM>

    {
        
        public FlightController(IFlightsService service, IMapper mapper) : base(service, mapper)
        {

        }





    }
}
