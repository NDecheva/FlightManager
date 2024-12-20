using FlightManager.Shared.Dtos;
using FlightManager.Shared.Repos.Contracts;
using FlightManager.Shared.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public class UsersService : BaseCrudService<UserDto, IUserRepository>, IUsersService
    {
        public UsersService(IUserRepository repository) : base(repository)
        {

        }

        public Task<bool> CanUserLoginAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task GetByUsernameAsync(object username)
        {
            throw new NotImplementedException();
        }
    }   
}
