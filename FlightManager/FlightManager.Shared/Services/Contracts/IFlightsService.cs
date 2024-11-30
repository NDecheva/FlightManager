using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Shared.Services.Contracts
{
    public interface IFlightsService : IBaseCrudService<FlightDto, IFlightRepository>

    {

    }
}
