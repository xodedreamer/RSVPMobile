using RSVPMobile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Services.Authentication
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginDto);
        Task<bool> SignupAsync(SignupRequest signupDto);
        Task LogoutAsync();
    }
}
