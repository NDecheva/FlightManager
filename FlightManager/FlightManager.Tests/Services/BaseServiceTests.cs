using FlightManager.Services;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManager.Tests.Services
{
    public abstract class BaseServiceTests<TModel, TRepository, TService>
            where TModel : BaseModel
            where TRepository : class, IBaseRepository<TModel>
            where TService : BaseCrudService<TModel, TRepository>
    {
        protected readonly Mock<TRepository> _repositoryMock;
        protected readonly TService _service;

        protected BaseServiceTests()
        {
            _repositoryMock = new Mock<TRepository>();
            _service = CreateService(_repositoryMock.Object);
        }

        protected abstract TService CreateService(TRepository repository);

        [Test]
        public async Task WhenGetAllAsync_ThenReturnAllModels()
        {
            // Arrange
            var models = new List<TModel>
                {
                    CreateModel(1),
                    CreateModel(2)
                };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(models);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            _repositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            Assert.AreEqual(models, result);
        }

        [Test]
        public async Task WhenGetWithPaginationAsync_ThenReturnPaginatedModels()
        {
            // Arrange
            var pageSize = 2;
            var pageNumber = 1;
            var models = new List<TModel>
                {
                    CreateModel(1),
                    CreateModel(2)
                };
            _repositoryMock.Setup(repo => repo.GetWithPaginationAsync(pageSize, pageNumber)).ReturnsAsync(models);

            // Act
            var result = await _service.GetWithPaginationAsync(pageSize, pageNumber);

            // Assert
            _repositoryMock.Verify(repo => repo.GetWithPaginationAsync(pageSize, pageNumber), Times.Once);
            Assert.AreEqual(models, result);
        }

        [Test]
        public async Task WhenExistsByIdAsync_WithExistingId_ThenReturnTrue()
        {
            // Arrange
            var modelId = 1;
            _repositoryMock.Setup(repo => repo.ExistsByIdAsync(modelId)).ReturnsAsync(true);

            // Act
            var result = await _service.ExistsByIdAsync(modelId);

            // Assert
            _repositoryMock.Verify(repo => repo.ExistsByIdAsync(modelId), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task WhenExistsByIdAsync_WithNonExistingId_ThenReturnFalse()
        {
            // Arrange
            var modelId = 1;
            _repositoryMock.Setup(repo => repo.ExistsByIdAsync(modelId)).ReturnsAsync(false);

            // Act
            var result = await _service.ExistsByIdAsync(modelId);

            // Assert
            Assert.IsFalse(result);
        }

        protected abstract TModel CreateModel(int id);
    }
}
