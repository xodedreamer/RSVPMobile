using System;
using System.Collections.Generic;
using System.Text;

namespace RSVPMobile.Dtos
{
    public record AuthResponse(
    string Token,
    string FullName,
    string Email,
    string Role // Important for identifying if the user is an Admin
);
}
