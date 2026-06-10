using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public record SignupRequest(string fullName, string email, string passwordHash, string role, string phoneNumber);
}
