using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using Moq;
using AutoMapper;

namespace FlightManager.Tests.Repos
{
    public class BaseRepositoryTests<TRepository,T,TModel>
        where TRepository : BaseRepository<T,TModel>
        where T : class , IBaseEntity
        where TModel : BaseModel
    {
        private Mock<FlightManagerDbContext> mockContext;
        private Mock<DbSet<T>> mockDbSet;
        private Mock<IMapper> mockMapper;
        private TRepository repository;
        [SetUp]
        public void Setup()
        {
            mockContext = new Mock<FlightManagerDbContext>();
            mockDbSet= new Mock<DbSet<T>>();
            mockMapper = new Mock<IMapper>();
            repository = new Mock<TRepository>(mockContext.Object, mockMapper.Object)
            { CallBase = true }.Object;
        }
        [Test]
        public void MapToModel_ValidEntity_ReturnsMappedModel()
        {
            //arange
            var entity = new Mock<T>();
            var model = new Mock<TModel>();
            mockMapper.Setup(m => m.Map<TModel>(entity.Object)).Returns(model.Object);
            var result=repository.MapToModel(entity.Object);
            Assert.That(result, Is.EqualTo(model.Object));
        }
    }

}
