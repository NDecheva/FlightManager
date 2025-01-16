using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Tests.RepositoryTests
{
    public class FlightRepositoryTests :
        BaseRepositoryTests<FlightRepository, Flight, FlightDto>
    {
        [Test]
        public async Task GetFlightById_ReturnsCorrectFlight()
        {
            //arrange
            var flightId = 1;
            var flight = new Flight { Id = flightId, DepartureLocation = "testuser", ArrivalLocation = "Example", AircraftId = 1, PilotName = "Example", PassengerCapacity = 1, BusinessClassCapacity = 1 };
            var flightDto = new FlightDto { Id = flightId, DepartureLocation = "testuser", ArrivalLocation = "Example", AircraftId = 1, PilotName = "Example", PassengerCapacity = 1, BusinessClassCapacity = 1 };
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Flights.Add(flight);
                await context.SaveChangesAsync();

            }
            mockMapper.Setup(m => m.Map<FlightDto>(It.IsAny<Flight>())).Returns(flightDto);
            FlightDto result;
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var flightRepository = new FlightRepository(context, mockMapper.Object);
                result = await flightRepository.GetByIdAsync(flightId);
            }
            Assert.NotNull(result);
            Assert.AreEqual(flightDto.Id, result.Id);
            Assert.AreEqual(flightDto.DepartureLocation, result.DepartureLocation);
            Assert.AreEqual(flightDto.ArrivalLocation, result.ArrivalLocation);
            Assert.AreEqual(flightDto.AircraftId, result.AircraftId);
            Assert.AreEqual(flightDto.PilotName, result.PilotName);
            Assert.AreEqual(flightDto.PassengerCapacity, result.PassengerCapacity);
            Assert.AreEqual(flightDto.BusinessClassCapacity, result.BusinessClassCapacity);


        }
    }
}
