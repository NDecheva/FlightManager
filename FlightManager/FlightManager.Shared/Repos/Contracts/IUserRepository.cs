﻿using FlightManager.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManager.Shared.Repos.Contracts
{
    public interface IUserRepository : IBaseRepository<UserDto>
    {
        Task<bool> CanUserLoginAsync(string username, string password);
        Task<UserDto> GetByUsernameAsync(string username);
    }

}
