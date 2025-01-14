using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FlightManager.Tests.RepositoryTests
{
    public class BaseRepositoryTests<TRepository, T, TModel>
        where TRepository : BaseRepository<T, TModel>
        where T : class, IBaseEntity
        where TModel : BaseModel
    {
        public Mock<FlightManagerDbContext> mockContext;
        public Mock<DbSet<T>> mockDbSet;
        public Mock<IMapper> mockMapper;
        public TRepository repository;
        protected DbContextOptions<FlightManagerDbContext> dbContextOptions;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<FlightManagerDbContext>();
            mockDbSet = new Mock<DbSet<T>>();
            mockMapper = new Mock<IMapper>();
            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);
            repository = new Mock<TRepository>(mockContext.Object, mockMapper.Object)
            { CallBase = true }.Object;
            dbContextOptions = new DbContextOptionsBuilder<FlightManagerDbContext>()
                             .UseInMemoryDatabase("TestDatabase")
                             .Options;
        }

        [Test]
        public void MapToModel_ValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new Mock<T>();
            var model = new Mock<TModel>();
            mockMapper.Setup(m => m.Map<TModel>(entity.Object)).Returns(model.Object);

            // Act
            var result = repository.MapToModel(entity.Object);

            // Assert
            Assert.That(result, Is.EqualTo(model.Object));
        }

        [Test]
        public void MapToEntity_ValidModel_ReturnsMappedEntity()
        {
            // Arrange
            var model = new Mock<TModel>();
            var entity = new Mock<T>();
            mockMapper.Setup(m => m.Map<T>(model.Object)).Returns(entity.Object);

            // Act
            var result = repository.MapToEntity(model.Object);

            // Assert
            Assert.That(result, Is.EqualTo(entity.Object));
        }

        [Test]
        public void MapToEnumerableOfModel_ValidEntities_ReturnsMappedModels()
        {
            // Arrange
            var entities = new List<T> { new Mock<T>().Object, new Mock<T>().Object };
            var models = new List<TModel> { new Mock<TModel>().Object, new Mock<TModel>().Object };
            mockMapper.Setup(m => m.Map<IEnumerable<TModel>>(entities)).Returns(models);

            // Act
            var result = repository.MapToEnumerableOfModel(entities);

            // Assert
            Assert.That(result, Is.EqualTo(models));
        }




        [Test]
        public void Dispose_DisposesContext()
        {
            // Act
            repository.Dispose();

            // Assert
            mockContext.Verify(c => c.Dispose(), Times.Once);
        }
    }
}