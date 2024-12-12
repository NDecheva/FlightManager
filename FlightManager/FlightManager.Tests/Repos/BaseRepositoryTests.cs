using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace FlightManager.Tests.Repos
{
    public class BaseRepositoryTests<TRepository, T, TModel>
        where TRepository : BaseRepository<T, TModel>
        where T : class, IBaseEntity, new()
        where TModel : BaseModel, new()
    {
        private Mock<FlightManagerDbContext> mockContext;
        private Mock<DbSet<T>> mockDbSet;
        private Mock<IMapper> mockMapper;
        private TRepository repository;

        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<FlightManagerDbContext>();
            mockDbSet = new Mock<DbSet<T>>();
            mockMapper = new Mock<IMapper>();

            mockContext.Setup(c => c.Set<T>()).Returns(mockDbSet.Object);
            repository = new Mock<TRepository>(mockContext.Object, mockMapper.Object)
            { CallBase = true }.Object;
        }

        [Test]
        public void MapToModel_ValidEntity_ReturnsMappedModel()
        {
            // Arrange
            var entity = new T();
            var model = new TModel();
            mockMapper.Setup(m => m.Map<TModel>(entity)).Returns(model);

            // Act
            var result = repository.MapToModel(entity);

            // Assert
            Assert.That(result, Is.EqualTo(model));
        }

        [Test]
        public void MapToEntity_ValidModel_ReturnsMappedEntity()
        {
            // Arrange
            var entity = new T();
            var model = new TModel();
            mockMapper.Setup(m => m.Map<T>(model)).Returns(entity);

            // Act
            var result = repository.MapToEntity(model);

            // Assert
            Assert.That(result, Is.EqualTo(entity));
        }

        

    

        [Test]
        public async Task CreateAsync_ValidModel_AddsEntityToDbSet()
        {
            // Arrange
            var model = new TModel();
            var entity = new T();

            mockMapper.Setup(m => m.Map<T>(model)).Returns(entity);

            // Act
            await repository.CreateAsync(model);

            // Assert
            mockDbSet.Verify(m => m.AddAsync(It.Is<T>(e => e == entity), default), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_EntityExists_RemovesEntityFromDbSet()
        {
            // Arrange
            var entity = new T();

            mockDbSet.Setup(m => m.FindAsync(1)).ReturnsAsync(entity);

            // Act
            await repository.DeleteAsync(1);

            // Assert
            mockDbSet.Verify(m => m.Remove(entity), Times.Once);
            mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

     
        





       
    }
}