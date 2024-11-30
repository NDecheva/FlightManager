using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;

namespace FlightManager.Data.Repos
{
    [AutoBind]
    // TO DO:
    public class FlightRepository : BaseRepository<Flight, FlightDto>, IFlightRepository
    {
        public FlightRepository(FlightManagerDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
