using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        [Test]
        public async Task CreateAsync_ShouldAddEntityToDbSet_WhenModelIsValid_ForBooking()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Id = 2,
                PersonalId = "testuser",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                PhoneNumber = "Example",
                Nationality = "Example",
                FlightId = 2
            };

            var bookingEntity = new Booking
            {
                Id = 2,
                PersonalId = "testuser",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                PhoneNumber = "Example",
                Nationality = "Example",
                FlightId = 2
            };

            mockMapper.Setup(m => m.Map<Booking>(It.IsAny<BookingDto>())).Returns(bookingEntity);

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var bookingRepository = new BookingRepository(context, mockMapper.Object);
                await bookingRepository.CreateAsync(bookingDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var createdBooking = await context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingEntity.Id);

                Assert.NotNull(createdBooking);
                Assert.AreEqual(bookingEntity.PersonalId, createdBooking.PersonalId);
                Assert.AreEqual(bookingEntity.FirstName, createdBooking.FirstName);
                Assert.AreEqual(bookingEntity.LastName, createdBooking.LastName);
                Assert.AreEqual(bookingEntity.MiddleName, createdBooking.MiddleName);
                Assert.AreEqual(bookingEntity.PhoneNumber, createdBooking.PhoneNumber);
                Assert.AreEqual(bookingEntity.Nationality, createdBooking.Nationality);
                Assert.AreEqual(bookingEntity.FlightId, createdBooking.FlightId);
            }
        }
        [Test]
        public async Task UpdateAsync_ShouldUpdateEntityInDbSet_WhenModelIsValid_ForBooking()
        {
            // Arrange
            var existingBooking = new Booking
            {
                Id = 3,
                PersonalId = "testuser",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                PhoneNumber = "Example",
                Nationality = "Example",
                FlightId = 3
            };

            var updatedBookingDto = new BookingDto
            {
                Id = 3,
                PersonalId = "testuser",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                PhoneNumber = "Example",
                Nationality = "Example",
                FlightId = 3
            };

            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Bookings.Add(existingBooking);
                await context.SaveChangesAsync();
            }

            mockMapper.Setup(m => m.Map<Booking>(It.IsAny<BookingDto>())).Returns((BookingDto dto) => new Booking
            {
                Id = dto.Id,
                PersonalId = dto.PersonalId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                PhoneNumber = dto.PhoneNumber,
                Nationality = dto.Nationality,
                FlightId=dto.FlightId
            });

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var bookingRepository = new BookingRepository(context, mockMapper.Object);
                await bookingRepository.UpdateAsync(updatedBookingDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var bookingFromDb = await context.Bookings.FirstOrDefaultAsync(f => f.Id == existingBooking.Id);

                Assert.NotNull(bookingFromDb);
                Assert.AreEqual(updatedBookingDto.PersonalId, bookingFromDb.PersonalId);
                Assert.AreEqual(updatedBookingDto.FirstName, bookingFromDb.FirstName);
                Assert.AreEqual(updatedBookingDto.LastName, bookingFromDb.LastName);
                Assert.AreEqual(updatedBookingDto.MiddleName, bookingFromDb.MiddleName);
                Assert.AreEqual(updatedBookingDto.PhoneNumber, bookingFromDb.PhoneNumber);
                Assert.AreEqual(updatedBookingDto.Nationality, bookingFromDb.Nationality);
                Assert.AreEqual(updatedBookingDto.FlightId, bookingFromDb.FlightId);
            }
        }
        [Test]
        public async Task SaveAsync_ShouldSaveUpdatedBookingToDatabase()
        {
            // Arrange
            var booking = new Booking
            {
                Id = 4,
                PersonalId = "testuser",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                PhoneNumber = "Example",
                Nationality = "Example",
                FlightId = 4
            };

            var bookingDto = new BookingDto
            {
                Id = 4,
                PersonalId = "testuser",
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                PhoneNumber = "Example",
                Nationality = "Example",
                FlightId = 4
            };

            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Bookings.Add(booking);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var bookingRepository = new BookingRepository(context, mockMapper.Object);
                mockMapper.Setup(m => m.Map<Booking>(bookingDto)).Returns(booking);
                await bookingRepository.SaveAsync(bookingDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var bookingFromDb = await context.Bookings.FirstOrDefaultAsync(f => f.Id == bookingDto.Id);

                Assert.NotNull(bookingFromDb);
                Assert.AreEqual(bookingDto.PersonalId, bookingFromDb.PersonalId);
                Assert.AreEqual(bookingDto.FirstName, bookingFromDb.FirstName);
                Assert.AreEqual(bookingDto.LastName, bookingFromDb.LastName);
                Assert.AreEqual(bookingDto.MiddleName, bookingFromDb.MiddleName);
                Assert.AreEqual(bookingDto.PhoneNumber, bookingFromDb.PhoneNumber);
                Assert.AreEqual(bookingDto.Nationality, bookingFromDb.Nationality);
                Assert.AreEqual(bookingDto.FlightId, bookingFromDb.FlightId);
            }
        }



    }
}

