using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RSVPMobile.Dtos
{
    public record LoginRequest(string email, string password);
}
