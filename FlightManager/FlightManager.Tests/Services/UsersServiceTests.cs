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
    public class UsersServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly IUsersService _service;

        public UsersServiceTests()
        {
            _service = new UsersService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var userDto = new UserDto()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.tst",
                PhoneNumber = "123456789",
                Password = "Test123!",
                PersonalId = "321123321",
                Address = "test",
                Role = UserRole.Admin,
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


            // Act
            await _service.SaveAsync(userDto);

            // Assert
            _userRepositoryMock.Verify(x => x.SaveAsync(userDto), Times.Once());
        }

        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowArgumentNullException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _userRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
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
            _userRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once);
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidUserId_ThenReturnUser(int userId)
        {
            // Arrange
            var userDto = new UserDto()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.tst",
                PhoneNumber = "123456789",
                Password = "Test123!",
                PersonalId = "321123321",
                Address = "test",
                Role = UserRole.Admin,
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
            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(userId))))
                .ReturnsAsync(userDto);

            // Act
            var userResult = await _service.GetByIdIfExistsAsync(userId);

            // Assert
            _userRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once);
            Assert.That(userResult == userDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidUserId_ThenReturnDefault(int userId)
        {
            // Arrange
            var user = (UserDto)default;
            _userRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(userId))))
                .ReturnsAsync(user);

            // Act
            var userResult = await _service.GetByIdIfExistsAsync(userId);

            // Assert
            _userRepositoryMock.Verify(x => x.GetByIdAsync(userId), Times.Once);
            Assert.That(userResult == user);
        }

        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var userDto = new UserDto()
            {
                UserName = "Test",
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.tst",
                PhoneNumber = "123456789",
                Password = "Test123!",
                PersonalId = "321123321",
                Address = "test",
                Role = UserRole.Admin,
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
            _userRepositoryMock.Setup(s => s.SaveAsync(It.Is<UserDto>(x => x.Equals(userDto))))
               .Verifiable();

            // Act
            await _service.SaveAsync(userDto);

            // Assert
            _userRepositoryMock.Verify(x => x.SaveAsync(userDto), Times.Once);
        }
    }
}
