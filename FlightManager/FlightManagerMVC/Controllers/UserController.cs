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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PetShelter.Shared.Security;
using System.Data;

namespace FlightManagerMVC.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UserController : BaseCrudController<UserDto, IUserRepository, IUsersService, UserEditVM, UserDetailsVM>
    {
        protected readonly IBookingsService _bookingService;
        protected readonly IUsersService _userService;
        
        public UserController(IUsersService service,IMapper mapper): base(service, mapper)
        {
                _userService = service;
        }

        protected override async Task<UserEditVM> PrePopulateVMAsync(UserEditVM editVM)
        {
                editVM.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>()
                .Select(role => new SelectListItem($"{role.ToString()}", ((int)role).ToString()));

            return editVM;
        }

        public override Task<IActionResult> Create(UserEditVM editVM)
        {
            editVM.Password = PasswordHasher.HashPassword(editVM.Password);
            return base.Create(editVM);
        }
        [HttpGet]
        [Route("User/Search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            ViewBag.SearchTerm = searchTerm;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(List));
            }

            var users = await _userService.GetAllAsync();

            var filteredUsers = users.Where(u =>
                u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            var userVMs = _mapper.Map<IEnumerable<UserDetailsVM>>(filteredUsers);

            ViewBag.UserVMs = userVMs;

            return View("List", userVMs);
        }

    }
}