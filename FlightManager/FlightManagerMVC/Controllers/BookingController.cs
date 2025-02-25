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
            // Initialize lists
            editVM.SeatClassesList = new List<SelectListItem>();
            editVM.FlightsList = new List<SelectListItem>();
            editVM.UsersList = new List<SelectListItem>();

            // Populate SeatClassesList
            editVM.SeatClassesList = Enum.GetValues(typeof(SeatClass))
                .Cast<SeatClass>()
                .Select(seatClass => new SelectListItem($"{seatClass}", ((int)seatClass).ToString()));

            // Populate FlightsList
            var flights = await _flightService.GetAllAsync();
            if (flights != null && flights.Any())
            {
                editVM.FlightsList = flights.Select(x => 
                    new SelectListItem($"{x.AircraftId} | {x.DepartureLocation} - {x.ArrivalLocation}", x.Id.ToString()));
            }
            else
            {
                editVM.FlightsList = new List<SelectListItem> 
                { 
                    new SelectListItem("No flights available", "0") 
                };
            }

            // Populate UsersList
            var users = await _userService.GetAllAsync();
            editVM.UsersList = users.Select(x => 
                new SelectListItem($"{x.FirstName} {x.LastName} ({x.UserName})", x.Id.ToString()));

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

            var bookings = await _bookingService.GetAllAsync();
            var filteredBookings = bookings.Where(u =>
                u.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.PhoneNumber.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                u.Nationality.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();


            var bookingsVMs = _mapper.Map<IEnumerable<BookingDetailsVM>>(filteredBookings);

            ViewBag.BookingVMs = bookingsVMs;

            return View("List", bookingsVMs);
        }

        protected override async Task<string?> Validate(BookingEditVM editVM)
        {
            // Get the selected user's data
            var user = await _userService.GetByIdIfExistsAsync(editVM.UserId);
            if (user == null)
            {
                return "Selected user not found";
            }

            // Verify the flight exists
            var flight = await _flightService.GetByIdIfExistsAsync(editVM.FlightId);
            if (flight == null)
            {
                return "Selected flight not found";
            }

            // Populate editVM with user data
            editVM.FirstName = user.FirstName;
            editVM.LastName = user.LastName;
            editVM.PhoneNumber = user.PhoneNumber;
            editVM.PersonalId = user.PersonalId;
            editVM.Nationality = string.Empty;
            editVM.SeatClass = editVM.SeatClass;
            editVM.FlightId = editVM.FlightId;
            editVM.UserId = editVM.UserId;

            return null;
        }

        public override async Task<IActionResult> Create(BookingEditVM editVM)
        {
            var errors = await Validate(editVM);
            if (errors != null)
            {
                await PrePopulateVMAsync(editVM);
                return View(editVM);
            }

            try
            {
                // Now map the populated editVM to DTO
                var bookingDto = _mapper.Map<BookingDto>(editVM);

                await _service.SaveAsync(bookingDto);
                return await List();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed to create booking: " + ex.Message);
                await PrePopulateVMAsync(editVM);
                return View(editVM);
            }


        }
    }
}
