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
                Id = 2,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 2,
                PilotName = "Example",
                PassengerCapacity = 2,
                BusinessClassCapacity = 2 
            };

            var flightEntity = new Flight
            {
                Id = 2,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 2,
                PilotName = "Example",
                PassengerCapacity = 2,
                BusinessClassCapacity = 2
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
        [Test]
        public async Task UpdateAsync_ShouldUpdateEntityInDbSet_WhenModelIsValid_ForFlight()
        {
            // Arrange
            var existingFlight = new Flight
            {
                Id = 3,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 3,
                PilotName = "Example",
                PassengerCapacity = 3,
                BusinessClassCapacity = 3
            };

            var updatedFlightDto = new FlightDto
            {
                Id = 3,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 3,
                PilotName = "Example",
                PassengerCapacity = 3,
                BusinessClassCapacity = 3
            };

            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Flights.Add(existingFlight);
                await context.SaveChangesAsync();
            }

            mockMapper.Setup(m => m.Map<Flight>(It.IsAny<FlightDto>())).Returns((FlightDto dto) => new Flight
            {
                Id = dto.Id,
                DepartureTime = dto.DepartureTime,
                ArrivalTime = dto.ArrivalTime,
                AircraftId = dto.AircraftId,
                PilotName = dto.PilotName,
                PassengerCapacity = dto.PassengerCapacity,
                BusinessClassCapacity = dto.BusinessClassCapacity
            });

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var flightRepository = new FlightRepository(context, mockMapper.Object);
                await flightRepository.UpdateAsync(updatedFlightDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var flightFromDb = await context.Flights.FirstOrDefaultAsync(f => f.Id == existingFlight.Id);

                Assert.NotNull(flightFromDb);
                Assert.AreEqual(updatedFlightDto.DepartureLocation, flightFromDb.DepartureLocation);
                Assert.AreEqual(updatedFlightDto.ArrivalLocation, flightFromDb.ArrivalLocation);
                Assert.AreEqual(updatedFlightDto.AircraftId, flightFromDb.AircraftId);
                Assert.AreEqual(updatedFlightDto.PilotName, flightFromDb.PilotName);
                Assert.AreEqual(updatedFlightDto.PassengerCapacity, flightFromDb.PassengerCapacity);
                Assert.AreEqual(updatedFlightDto.BusinessClassCapacity, flightFromDb.BusinessClassCapacity);
            }
        }
        [Test]
        public async Task SaveAsync_ShouldSaveUpdatedFlightToDatabase()
        {
            // Arrange
            var flight = new Flight
            {
                Id = 4,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 4,
                PilotName = "Example",
                PassengerCapacity = 4,
                BusinessClassCapacity = 4
            };

            var flightDto = new FlightDto
            {
                Id = 4,
                DepartureLocation = "testuser",
                ArrivalLocation = "Example",
                AircraftId = 4,
                PilotName = "Example",
                PassengerCapacity = 4,
                BusinessClassCapacity = 4
            };

            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Flights.Add(flight);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var flightRepository = new FlightRepository(context, mockMapper.Object);
                mockMapper.Setup(m => m.Map<Flight>(flightDto)).Returns(flight);
                await flightRepository.SaveAsync(flightDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var flightFromDb = await context.Flights.FirstOrDefaultAsync(f => f.Id == flightDto.Id);

                Assert.NotNull(flightFromDb);
                Assert.AreEqual(flightDto.DepartureLocation, flightFromDb.DepartureLocation);
                Assert.AreEqual(flightDto.ArrivalLocation, flightFromDb.ArrivalLocation);
                Assert.AreEqual(flightDto.AircraftId, flightFromDb.AircraftId);
                Assert.AreEqual(flightDto.PilotName, flightFromDb.PilotName);
                Assert.AreEqual(flightDto.PassengerCapacity, flightFromDb.PassengerCapacity);
                Assert.AreEqual(flightDto.BusinessClassCapacity, flightFromDb.BusinessClassCapacity);
            }
        }




    }
}
