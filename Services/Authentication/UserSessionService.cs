using RSVPMobile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Services.Authentication
{
    public class UserSessionService : IUserSessionService
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MemberSince { get; set; }

        public void LoadFromLoginResponse(AuthResponse response)
        {
            UserName = response.FullName;
            Email = response.Email;
            PhoneNumber = response.PhoneNumber;
            MemberSince = response.CreatedAt;
        }

        public void Clear()
        {
            UserName = null;
            Email = null;
            PhoneNumber = null;

        }
    }
}
