using AutoMapper;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using FlightManager.Shared.Services.Contracts;
using FlightManagerMVC.Enums;
using FlightManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin , User")]
    public class FlightController : BaseCrudController<FlightDto, IFlightRepository, IFlightsService, FlightEditVM, FlightDetailsVM>

    {
        
        public FlightController(IFlightsService service, IMapper mapper) : base(service, mapper)
        {

        }


        protected override async Task<FlightEditVM> PrePopulateVMAsync(FlightEditVM editVM)
        {
            editVM.AircraftTypes = Enum.GetValues(typeof(AircraftType)).Cast<AircraftType>()
            .Select(type => new SelectListItem($"{type.ToString()}", ((int)type).ToString()));

            return editVM;
        }


    }
}
