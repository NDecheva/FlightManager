using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using FlightManager.Shared.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    [AutoBind]
    public class BookingsService : BaseCrudService<BookingDto, IBookingRepository>, IBookingsService
    {
        public BookingsService(IBookingRepository repository) : base(repository)
        {
        }
    }
}
