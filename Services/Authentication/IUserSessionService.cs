using RSVPMobile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Services.Authentication
{
    public interface IUserSessionService
    {
        string UserName { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        DateTime MemberSince { get; set; }
 
        void LoadFromLoginResponse(AuthResponse response);
        void Clear();
    }
}
