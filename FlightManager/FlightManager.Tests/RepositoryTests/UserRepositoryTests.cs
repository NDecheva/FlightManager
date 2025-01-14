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
    public class UserRepositoryTests :
        BaseRepositoryTests<UserRepository, User, UserDto>
    {
        [Test]
        public async Task GetUserById_ReturnsCorrectUser()
        {
            //arrange
            var userId = 1;
            var user = new User { Id = userId, UserName = "testuser", FirstName = "Example", LastName = "Example", Email = "Example", PhoneNumber = "Example", Password = "password", PersonalId = "Example", Address = "Example" };
            var userDto = new UserDto { Id = userId, UserName = "testuser", FirstName = "Example", LastName = "Example", Email = "Example", PhoneNumber = "Example", Password = "password", PersonalId = "Example", Address = "Example" };
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();

            }
            mockMapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(userDto);
            UserDto result;
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var userRepository = new UserRepository(context, mockMapper.Object);
                result = await userRepository.GetByIdAsync(userId);
            }
            Assert.NotNull(result);
            Assert.AreEqual(userDto.Id, result.Id);
            Assert.AreEqual(userDto.UserName, result.UserName);
            Assert.AreEqual(userDto.FirstName, result.FirstName);
            Assert.AreEqual(userDto.LastName, result.LastName);
            Assert.AreEqual(userDto.Email, result.Email);
            Assert.AreEqual(userDto.PhoneNumber, result.PhoneNumber);
            Assert.AreEqual(userDto.PersonalId, result.PersonalId);
            Assert.AreEqual(userDto.Address, result.Address);



        }
    }
}
