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



    public class BookingRepositoryTests :
        BaseRepositoryTests<BookingRepository, Booking, BookingDto>
    {
        [Test]
        public async Task GetBookingById_ReturnsCorrectBooking()
        {
            //arrange
            var bookingId = 1;
            var booking = new Booking { Id = bookingId, PersonalId = "testuser", FirstName = "Example", LastName = "Example", MiddleName = "Example", PhoneNumber = "Example", Nationality = "Example", FlightId = 1 };
            var bookingDto = new BookingDto { Id = bookingId, PersonalId = "testuser", FirstName = "Example", LastName = "Example", MiddleName = "Example", PhoneNumber = "Example", Nationality = "Example", FlightId = 1 };
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Bookings.Add(booking);
                await context.SaveChangesAsync();

            }
            mockMapper.Setup(m => m.Map<BookingDto>(It.IsAny<Booking>())).Returns(bookingDto);
            BookingDto result;
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var bookingRepository = new BookingRepository(context, mockMapper.Object);
                result = await bookingRepository.GetByIdAsync(bookingId);
            }
            Assert.NotNull(result);
            Assert.AreEqual(bookingDto.Id, result.Id);
            Assert.AreEqual(bookingDto.PersonalId, result.PersonalId);
            Assert.AreEqual(bookingDto.FirstName, result.FirstName);
            Assert.AreEqual(bookingDto.LastName, result.LastName);
            Assert.AreEqual(bookingDto.MiddleName, result.MiddleName);
            Assert.AreEqual(bookingDto.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(bookingDto.Nationality, result.Nationality);
            Assert.AreEqual(bookingDto.FlightId, result.FlightId);




        }
    }
}

