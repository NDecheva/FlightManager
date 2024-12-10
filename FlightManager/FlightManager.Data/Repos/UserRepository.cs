using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;

namespace FlightManager.Data.Repos
{
    [AutoBind]
    // TO DO:
    public class UserRepository : BaseRepository<User, UserDto>, IUserRepository
    {
        public UserRepository(FlightManagerDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
