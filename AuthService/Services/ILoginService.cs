﻿using AuthService.Dtos;

namespace AuthService.Services
{
    public interface ILoginService
    {
        Task<UserToken> Login(UserLoginPasswordDto userLogin);
    }
}
