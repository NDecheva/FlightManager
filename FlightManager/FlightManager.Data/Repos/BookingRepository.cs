using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;

namespace FlightManager.Data.Repos
{
    [AutoBind]
    // TO DO:
    public class BookingRepository : BaseRepository<Booking, BookingDto>, IBookingRepository
    {
        public BookingRepository(FlightManagerDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
