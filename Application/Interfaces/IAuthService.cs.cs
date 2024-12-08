﻿    using Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Application.Interfaces
    {
        public interface IAuthService
        {
            Task<bool> CreateUser(UserDto user);
        Task<UserDto> GetUserByName(string username);
    }
    }
