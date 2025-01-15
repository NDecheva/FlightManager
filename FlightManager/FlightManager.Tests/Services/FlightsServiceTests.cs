using FlightManager.Services;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using FlightManager.Shared.Services.Contracts;
using FlightManagerMVC.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Tests.Services
{
    public class FlightsServiceTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock = new Mock<IFlightRepository>();
        private readonly IFlightsService _service;

        public FlightsServiceTests()
        {
            _service = new FlightsService(_flightRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var flightDto = new FlightDto()
            {
                DepartureLocation = "test",
                ArrivalLocation = "test",
                DepartureTime = DateTime.UtcNow,
                ArrivalTime = DateTime.UtcNow,
                AircraftType = AircraftType.Helicopter,
                AircraftId = 1,
                PilotName = "test",
                PassengerCapacity = 3,
                BusinessClassCapacity = 3,
                Bookings = new List<BookingDto>
                {
                    new BookingDto
                    {
                        Id = 1,
                        PersonalId = "123123123",
                        FirstName = "test",
                        LastName = "testt",
                        MiddleName = "testtt",
                        PhoneNumber = "321123321",
                        Nationality = "mangal",
                        SeatClass = SeatClass.EconomyClass,
                        FlightId = 1,

                         Flight = new FlightDto
                            {
                                Id = 1,
                                DepartureLocation = "test",
                                ArrivalLocation = "test",
                                DepartureTime = DateTime.Now,
                                ArrivalTime = DateTime.Now,
                                AircraftType = AircraftType.Jet,
                                AircraftId = 1,
                                PilotName = "Test",
                                PassengerCapacity = 1,
                                BusinessClassCapacity = 1,
                            }
                    },

                    new BookingDto
                    {
                        Id = 2,
                        PersonalId = "123123123",
                        FirstName = "test",
                        LastName = "testt",
                        MiddleName = "testtt",
                        PhoneNumber = "321123321",
                        Nationality = "mangal",
                        SeatClass = SeatClass.EconomyClass,
                        FlightId = 2,

                         Flight = new FlightDto
                            {
                                Id = 2,
                                DepartureLocation = "test",
                                ArrivalLocation = "test",
                                DepartureTime = DateTime.Now,
                                ArrivalTime = DateTime.Now,
                                AircraftType = AircraftType.Jet,
                                AircraftId = 1,
                                PilotName = "Test",
                                PassengerCapacity = 1,
                                BusinessClassCapacity = 1,
                            }
                    }
                }
            };

            //Act
            await _service.SaveAsync(flightDto);

            //Assert
            _flightRepositoryMock.Verify(x => x.SaveAsync(flightDto), Times.Once());
        }

        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _flightRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());

        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            // Act
            await _service.DeleteAsync(id);

            // Assert
            _flightRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidFlightId_ThenReturnFlight(int flightId)
        {
            // Arrange
            var flightDto = new FlightDto()
            {
                DepartureLocation = "test",
                ArrivalLocation = "test",
                DepartureTime = DateTime.UtcNow,
                ArrivalTime = DateTime.UtcNow,
                AircraftType = AircraftType.Helicopter,
                AircraftId = 1,
                PilotName = "test",
                PassengerCapacity = 3,
                BusinessClassCapacity = 3,
                Bookings = new List<BookingDto>
                {
                    new BookingDto
                    {
                        Id = 1,
                        PersonalId = "123123123",
                        FirstName = "test",
                        LastName = "testt",
                        MiddleName = "testtt",
                        PhoneNumber = "321123321",
                        Nationality = "mangal",
                        SeatClass = SeatClass.EconomyClass,
                        FlightId = 1,

                         Flight = new FlightDto
                         {
                                Id = 1,
                                DepartureLocation = "test",
                                ArrivalLocation = "test",
                                DepartureTime = DateTime.Now,
                                ArrivalTime = DateTime.Now,
                                AircraftType = AircraftType.Jet,
                                AircraftId = 1,
                                PilotName = "Test",
                                PassengerCapacity = 1,
                                BusinessClassCapacity = 1,
                          }
                    },

                    new BookingDto
                    {
                        Id = 2,
                        PersonalId = "123123123",
                        FirstName = "test",
                        LastName = "testt",
                        MiddleName = "testtt",
                        PhoneNumber = "321123321",
                        Nationality = "mangal",
                        SeatClass = SeatClass.EconomyClass,
                        FlightId = 2,

                         Flight = new FlightDto
                         {
                                Id = 2,
                                DepartureLocation = "test",
                                ArrivalLocation = "test",
                                DepartureTime = DateTime.Now,
                                ArrivalTime = DateTime.Now,
                                AircraftType = AircraftType.Jet,
                                AircraftId = 1,
                                PilotName = "Test",
                                PassengerCapacity = 1,
                                BusinessClassCapacity = 1,
                         }
                    }
                }
            };
            _flightRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(flightId))))
                .ReturnsAsync(flightDto);

            // Act
            var flightResult = await _service.GetByIdIfExistsAsync(flightId);

            // Assert
            _flightRepositoryMock.Verify(x => x.GetByIdAsync(flightId), Times.Once);
            Assert.That(flightResult == flightDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidFlightId_ThenReturnDefault(int flightId)
        {
            // Arrange
            var flight = (FlightDto)default;
            _flightRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(flightId))))
                .ReturnsAsync(flight);

            // Act
            var flightResult = await _service.GetByIdIfExistsAsync(flightId);

            // Assert
            _flightRepositoryMock.Verify(x => x.GetByIdAsync(flightId), Times.Once);
            Assert.That(flightResult == flight);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var flightDto = new FlightDto()
            {
                DepartureLocation = "test",
                ArrivalLocation = "test",
                DepartureTime = DateTime.UtcNow,
                ArrivalTime = DateTime.UtcNow,
                AircraftType = AircraftType.Helicopter,
                AircraftId = 1,
                PilotName = "test",
                PassengerCapacity = 3,
                BusinessClassCapacity = 3,
                Bookings = new List<BookingDto>
                {
                    new BookingDto
                    {
                        Id = 1,
                        PersonalId = "123123123",
                        FirstName = "test",
                        LastName = "testt",
                        MiddleName = "testtt",
                        PhoneNumber = "321123321",
                        Nationality = "mangal",
                        SeatClass = SeatClass.EconomyClass,
                        FlightId = 1,

                         Flight = new FlightDto
                         {
                                Id = 1,
                                DepartureLocation = "test",
                                ArrivalLocation = "test",
                                DepartureTime = DateTime.Now,
                                ArrivalTime = DateTime.Now,
                                AircraftType = AircraftType.Jet,
                                AircraftId = 1,
                                PilotName = "Test",
                                PassengerCapacity = 1,
                                BusinessClassCapacity = 1,
                          }
                    },

                    new BookingDto
                    {
                        Id = 2,
                        PersonalId = "123123123",
                        FirstName = "test",
                        LastName = "testt",
                        MiddleName = "testtt",
                        PhoneNumber = "321123321",
                        Nationality = "mangal",
                        SeatClass = SeatClass.EconomyClass,
                        FlightId = 2,

                         Flight = new FlightDto
                         {
                                Id = 2,
                                DepartureLocation = "test",
                                ArrivalLocation = "test",
                                DepartureTime = DateTime.Now,
                                ArrivalTime = DateTime.Now,
                                AircraftType = AircraftType.Jet,
                                AircraftId = 1,
                                PilotName = "Test",
                                PassengerCapacity = 1,
                                BusinessClassCapacity = 1,
                         }
                    }
                }
            };
            _flightRepositoryMock.Setup(s => s.SaveAsync(It.Is<FlightDto>(x => x.Equals(flightDto))))
               .Verifiable();

            // Act
            await _service.SaveAsync(flightDto);

            // Assert
            _flightRepositoryMock.Verify(x => x.SaveAsync(flightDto), Times.Once);
        }
    }
}
