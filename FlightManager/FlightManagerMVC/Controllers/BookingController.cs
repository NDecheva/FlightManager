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
    [AllowAnonymous]

    public class BookingController : BaseCrudController<BookingDto, IBookingRepository, IBookingsService, BookingEditVM, BookingDetailsVM>
    {
        protected readonly IFlightsService _flightService;

        public BookingController(IBookingsService service, IFlightsService flightService, IMapper mapper) : base(service, mapper)
        {
            this._flightService = flightService;
        }

        protected override async Task<BookingEditVM> PrePopulateVMAsync(BookingEditVM editVM)
        {
            editVM.SeatClassesList = Enum.GetValues(typeof(SeatClass)).Cast<SeatClass>()
            .Select(seatClass => new SelectListItem($"{seatClass.ToString()}", ((int)seatClass).ToString()));

            editVM.FlightsList = (await _flightService.GetAllAsync())
.Select(x => new SelectListItem($"{x.AircraftId} | {x.DepartureLocation} - {x.ArrivalLocation}", x.Id.ToString()));

            return editVM;
        }
    }
}
