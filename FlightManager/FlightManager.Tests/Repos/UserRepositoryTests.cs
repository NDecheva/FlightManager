using FlightManager.Data.Entities;
using FlightManager.Data.Repos;
using FlightManager.Shared.Dtos;

namespace FlightManager.Tests.Repos
{
    public class UserRepositoryTests :
         BaseRepositoryTests<UserRepository, User, UserDto>
    {
    }
}
