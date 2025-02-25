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
    public class BookingsServiceTests
    {
        private readonly Mock<IBookingRepository> _bookingsRepositoryMock = new Mock<IBookingRepository>();
        private readonly IBookingsService _service;

        public BookingsServiceTests()
        {
            _service = new BookingsService(_bookingsRepositoryMock.Object);
        }

        [Test]
        public async Task WhenCreateAsync_WithValidData_ThenSaveAsync()
        {
            //Arrange
            var bookingDto = new BookingDto
            {
                PersonalId = "09669699",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "123456789",
                Nationality = "Test",
                SeatClass = SeatClass.BusinessClass,
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
            };

            //Act
            await _service.SaveAsync(bookingDto);

            //Assert
            _bookingsRepositoryMock.Verify(x => x.SaveAsync(bookingDto), Times.Once());
        }

        [Test]
        public async Task WhenSaveAsync_WithNull_ThenThrowNullException()
        {
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _service.SaveAsync(null));
            _bookingsRepositoryMock.Verify(x => x.SaveAsync(null), Times.Never());
        }

        [Theory]
        [TestCase(1)]
        [TestCase(12)]
        [TestCase(123)]
        public async Task WhenDeleteAsync_WithCorrectId_ThenCallDeleteAsyncInRepository(int id)
        {
            //Arrange
            //Act
            await _service.DeleteAsync(id);

            //Assert
            _bookingsRepositoryMock.Verify(x => x.DeleteAsync(It.Is<int>(i => i.Equals(id))), Times.Once());
        }

        [Theory]
        [TestCase(1)]
        [TestCase(22)]
        [TestCase(131)]
        public async Task WhenGetByIdAsync_WithValidBookingId_ThenReturnBooking(int bookingId)
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                PersonalId = "09669699",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "123456789",
                Nationality = "Test",
                SeatClass = SeatClass.BusinessClass,
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
            };

            _bookingsRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(x => x.Equals(bookingId))))
                .ReturnsAsync(bookingDto);

            // Act
            var bookingResult = await _service.GetByIdIfExistsAsync(bookingId);

            // Assert
            _bookingsRepositoryMock.Verify(x => x.GetByIdAsync(bookingId), Times.Once());
            Assert.That(bookingResult == bookingDto);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(102021)]
        public async Task WhenGetByIdAsync_WithInvalidBookingId_ThenReturnDefault(int bookingId)
        {
            // Arrange
            var booking = (BookingDto)default;

            _bookingsRepositoryMock.Setup(s => s.GetByIdAsync(It.Is<int>(x => x.Equals(bookingId))))
                .ReturnsAsync(booking);

            // Act
            var bookingResult = await _service.GetByIdIfExistsAsync(bookingId);

            // Assert
            _bookingsRepositoryMock.Verify(x => x.GetByIdAsync(bookingId), Times.Once());
            Assert.That(bookingResult == booking);
        }


        [Test]
        public async Task WhenUpdateAsync_WithValidData_ThenSaveAsync()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                PersonalId = "09669699",
                FirstName = "Test",
                LastName = "Test",
                PhoneNumber = "123456789",
                Nationality = "Test",
                SeatClass = SeatClass.BusinessClass,
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
            };

            _bookingsRepositoryMock.Setup(s => s.SaveAsync(It.Is<BookingDto>(x => x.Equals(bookingDto))))
               .Verifiable();

            // Act
            await _service.SaveAsync(bookingDto);

            // Assert
            _bookingsRepositoryMock.Verify(x => x.SaveAsync(bookingDto), Times.Once);
        }
    }
}
