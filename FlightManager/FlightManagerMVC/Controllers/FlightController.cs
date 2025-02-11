using AutoMapper;
using FlightManager.Services;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using FlightManager.Shared.Services.Contracts;
using FlightManagerMVC.Enums;
using FlightManagerMVC.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin , User")]
    public class FlightController : BaseCrudController<FlightDto, IFlightRepository, IFlightsService, FlightEditVM, FlightDetailsVM>

    {
        protected readonly IFlightsService _flightService;
        public FlightController(IFlightsService service, IMapper mapper) : base(service, mapper)
        {
           _flightService = service;
        }


        protected override async Task<FlightEditVM> PrePopulateVMAsync(FlightEditVM editVM)
        {
            editVM.AircraftTypes = Enum.GetValues(typeof(AircraftType)).Cast<AircraftType>()
            .Select(type => new SelectListItem($"{type.ToString()}", ((int)type).ToString()));

            return editVM;
        }
        [HttpGet]
        [Route("Flight/Search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(List));
            }

            var flights = await _flightService.GetAllAsync();
            var filteredFlights = flights.Where(u =>
                u.DepartureLocation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.ArrivalLocation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                
                u.PilotName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();


            var flightsVMs = _mapper.Map<IEnumerable<FlightDetailsVM>>(filteredFlights);

            ViewBag.FlightsVMs = flightsVMs;

            return View("List", flightsVMs);
        }

    }
}
