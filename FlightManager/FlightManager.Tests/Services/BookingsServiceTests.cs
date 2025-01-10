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
                MiddleName = "Test",
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

    }
}
