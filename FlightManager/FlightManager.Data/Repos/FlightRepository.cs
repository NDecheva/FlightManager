using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;

namespace FlightManager.Data.Repos
{
    [AutoBind]
    
    public class FlightRepository : BaseRepository<Flight, FlightDto>, IFlightRepository
    {
        public FlightRepository(FlightManagerDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
