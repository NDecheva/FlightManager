using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;

namespace FlightManager.Tests.Repos
{
    public class FlightRepositoryTests :
         BaseRepositoryTests<FlightRepository, Flight, FlightDto>
    {
    }
}
