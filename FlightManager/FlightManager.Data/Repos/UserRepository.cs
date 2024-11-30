using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;

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
