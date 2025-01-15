using FlightManager.Services;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;

namespace FlightManager.Tests.Services
{
    public abstract class BaseServiceTests<TModel, TRepository, TService>
        where TModel : BaseModel
        where TRepository : IBaseRepository<TModel>
        where TService : BaseCrudService<TModel, TRepository>
    {
        protected readonly TRepository repository;
        protected BaseServiceTests(TRepository repository)
        {
            this.repository = repository;
        }
    }
}
