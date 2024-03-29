﻿using CarParts.Dto.User;
using CarParts.Models.Security;

namespace CarParts.Repoistory.AuthRepository
{
    public interface IAuthRepository
    {
        Task<string> Login(LoginUserDto userDto);
        void Logout();
        Task<string> Register(RegisterUserDto userDto);
        Task<User> CurrentUser();


    }
}
