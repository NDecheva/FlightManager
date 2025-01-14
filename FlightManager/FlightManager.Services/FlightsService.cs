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
    public class FlightsService : BaseCrudService<FlightDto, IFlightRepository>, IFlightsService
    {
        public FlightsService(IFlightRepository repository) : base(repository)
        {

        }
    }
}
