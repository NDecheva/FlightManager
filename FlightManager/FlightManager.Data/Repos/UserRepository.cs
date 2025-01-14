using AutoMapper;
using FlightManager.Data.Entities;
using FlightManager.Shared.Attributes;
using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using Microsoft.EntityFrameworkCore;
using PetShelter.Shared.Security;

namespace FlightManager.Data.Repos
{
    [AutoBind]
    
    public class UserRepository : BaseRepository<User, UserDto>, IUserRepository
    {
        public UserRepository(FlightManagerDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<bool> CanUserLoginAsync(string username, string password)
        {
            var hashedPassword = (await this.GetByUsernameAsync(username))?.Password;
            return PasswordHasher.VerifyPassword(password, hashedPassword);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            return MapToModel(await _dbSet.FirstOrDefaultAsync(u => u.UserName == username));
        }
    }
}
