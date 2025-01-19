using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [Test]
        public async Task CreateAsync_ShouldAddEntityToDbSet_WhenModelIsValid_ForUser()
        {
            // Arrange
            var userDto = new UserDto
            {
                Id = 2,
                UserName = "testuser",
                FirstName = "Example",
                LastName = "Example",
                Email = "Example",
                PhoneNumber = "Example",
                Password = "Example",
                PersonalId = "Example",
                Address= "Example"
            };

            var userEntity = new User
            {
                Id = 2,
                UserName = "testuser",
                FirstName = "Example",
                LastName = "Example",
                Email = "Example",
                PhoneNumber = "Example",
                Password = "Example",
                PersonalId = "Example",
                Address = "Example"
            };

            mockMapper.Setup(m => m.Map<User>(It.IsAny<UserDto>())).Returns(userEntity);

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var userRepository = new UserRepository(context, mockMapper.Object);
                await userRepository.CreateAsync(userDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var createdUser = await context.Users.FirstOrDefaultAsync(b => b.Id == userEntity.Id);

                Assert.NotNull(createdUser);
                Assert.AreEqual(userEntity.Id, createdUser.Id);
                Assert.AreEqual(userEntity.UserName, createdUser.UserName);
                Assert.AreEqual(userEntity.FirstName, createdUser.FirstName);
                Assert.AreEqual(userEntity.LastName, createdUser.LastName);
                Assert.AreEqual(userEntity.Email, createdUser.Email);
                Assert.AreEqual(userEntity.PhoneNumber, createdUser.PhoneNumber);
                Assert.AreEqual(userEntity.PersonalId, createdUser.PersonalId);
                Assert.AreEqual(userEntity.Address, createdUser.Address);
            }
        }
        [Test]
        public async Task UpdateAsync_ShouldUpdateEntityInDbSet_WhenModelIsValid_ForUser()
        {
            // Arrange
            var existingUser = new User
            {
                Id = 3,
                UserName = "testuser",
                FirstName = "Example",
                LastName = "Example",
                Email = "Example",
                PhoneNumber = "Example",
                Password = "Example",
                PersonalId = "Example",
                Address = "Example"
            };

            var updatedUserDto = new UserDto
            {
                Id = 3,
                UserName = "testuser",
                FirstName = "Example",
                LastName = "Example",
                Email = "Example",
                PhoneNumber = "Example",
                Password = "Example",
                PersonalId = "Example",
                Address = "Example"
            };

            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Users.Add(existingUser);
                await context.SaveChangesAsync();
            }

            mockMapper.Setup(m => m.Map<User>(It.IsAny<UserDto>())).Returns((UserDto dto) => new User
            {
                Id = dto.Id,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Password = dto.Password,
                PersonalId = dto.PersonalId,
                Address=dto.Address
            });

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var userRepository = new UserRepository(context, mockMapper.Object);
                await userRepository.UpdateAsync(updatedUserDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var userFromDb = await context.Users.FirstOrDefaultAsync(f => f.Id == existingUser.Id);

                Assert.NotNull(userFromDb);
                Assert.AreEqual(updatedUserDto.Id, userFromDb.Id);
                Assert.AreEqual(updatedUserDto.UserName, userFromDb.UserName);
                Assert.AreEqual(updatedUserDto.FirstName, userFromDb.FirstName);
                Assert.AreEqual(updatedUserDto.LastName, userFromDb.LastName);
                Assert.AreEqual(updatedUserDto.Email, userFromDb.Email);
                Assert.AreEqual(updatedUserDto.PhoneNumber, userFromDb.PhoneNumber);
                Assert.AreEqual(updatedUserDto.PersonalId, userFromDb.PersonalId);
                Assert.AreEqual(updatedUserDto.Address, userFromDb.Address);
            }
        }
        [Test]
        public async Task SaveAsync_ShouldSaveUpdatedUsersToDatabase()
        {
            // Arrange
            var user = new User
            {
                Id = 4,
                UserName = "testuser",
                FirstName = "Example",
                LastName = "Example",
                Email = "Example",
                PhoneNumber = "Example",
                Password = "Example",
                PersonalId = "Example",
                Address = "Example"
            };

            var userDto = new UserDto
            {
                Id = 4,
                UserName = "testuser",
                FirstName = "Example",
                LastName = "Example",
                Email = "Example",
                PhoneNumber = "Example",
                Password = "Example",
                PersonalId = "Example",
                Address = "Example"
            };

            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var userRepository = new UserRepository(context, mockMapper.Object);
                mockMapper.Setup(m => m.Map<User>(userDto)).Returns(user);
                await userRepository.SaveAsync(userDto);
            }

            // Assert
            using (var context = new FlightManagerDbContext(dbContextOptions))
            {
                var userFromDb = await context.Users.FirstOrDefaultAsync(f => f.Id == userDto.Id);

                Assert.NotNull(userFromDb);
                Assert.AreEqual(userDto.UserName, userFromDb.UserName);
                Assert.AreEqual(userDto.FirstName, userFromDb.FirstName);
                Assert.AreEqual(userDto.LastName, userFromDb.LastName);
                Assert.AreEqual(userDto.Email, userFromDb.Email);
                Assert.AreEqual(userDto.PhoneNumber, userFromDb.PhoneNumber);
                Assert.AreEqual(userDto.Password, userFromDb.Password);
                Assert.AreEqual(userDto.PersonalId, userFromDb.PersonalId);
                Assert.AreEqual(userDto.Address, userFromDb.Address);
            }
        }
    }
}
