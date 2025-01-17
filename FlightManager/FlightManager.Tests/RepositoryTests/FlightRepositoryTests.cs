using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
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
        [Test]
        public async Task CreateAsync_ShouldAddEntityToDbSet_WhenModelIsValid_ForFlight()
        {
            // Arrange
            var flightDto = new FlightDto
            {
                Id = 1,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 1,
                PilotName = "Example",
                PassengerCapacity = 1,
                BusinessClassCapacity = 1 
            };

            var flightEntity = new Flight
            {
                Id = 1,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 1,
                PilotName = "Example",
                PassengerCapacity = 1,
                BusinessClassCapacity = 1
            };

            mockMapper.Setup(m => m.Map<Flight>(It.IsAny<FlightDto>())).Returns(flightEntity);

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var flightRepository = new FlightRepository(context, mockMapper.Object);
                await flightRepository.CreateAsync(flightDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var createdFlight = await context.Flights.FirstOrDefaultAsync(f => f.Id == flightEntity.Id);

                Assert.NotNull(createdFlight);
                Assert.AreEqual(flightEntity.DepartureLocation, createdFlight.DepartureLocation);
                Assert.AreEqual(flightEntity.ArrivalLocation, createdFlight.ArrivalLocation);
                Assert.AreEqual(flightEntity.AircraftId, createdFlight.AircraftId);
                Assert.AreEqual(flightEntity.PilotName, createdFlight.PilotName);
                Assert.AreEqual(flightEntity.PassengerCapacity, createdFlight.PassengerCapacity);
                Assert.AreEqual(flightEntity.BusinessClassCapacity, createdFlight.BusinessClassCapacity);
            }
        }

    }
}
