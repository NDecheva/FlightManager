using AutoMapper;

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
    [AllowAnonymous]

    public class BookingController : BaseCrudController<BookingDto, IBookingRepository, IBookingsService, BookingEditVM, BookingDetailsVM>
    {
        protected readonly IFlightsService _flightService;
        protected readonly IBookingsService _bookingService;
        protected readonly IUsersService _userService;
        public BookingController(IBookingsService service, IFlightsService flightService,IUsersService usersService, IMapper mapper) : base(service, mapper)
        {
            this._flightService = flightService;
            this._bookingService = service;
            this._userService = usersService;
        }

        protected override async Task<BookingEditVM> PrePopulateVMAsync(BookingEditVM editVM)
        {
            editVM.SeatClassesList = Enum.GetValues(typeof(SeatClass)).Cast<SeatClass>()
            .Select(seatClass => new SelectListItem($"{seatClass.ToString()}", ((int)seatClass).ToString()));

            editVM.FlightsList = (await _flightService.GetAllAsync())
.Select(x => new SelectListItem($"{x.AircraftId} | {x.DepartureLocation} - {x.ArrivalLocation}", x.Id.ToString()));

            return editVM;
        }
        [HttpGet]
        [Route("Booking/Search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(List));
            }

            var bookings = await _userService.GetAllAsync();

            var filteredBookings = bookings.Where(u =>
                u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            var bookingVMs = _mapper.Map<IEnumerable<UserDetailsVM>>(filteredBookings);

            ViewBag.BookingVM = bookingVMs;

            return View("List", bookingVMs);
        }
    }
}
