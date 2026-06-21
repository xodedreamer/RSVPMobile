using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using RSVPMobile.Dtos;
using Microsoft.Maui.Graphics;

namespace RSVPMobile.Models
{
    public class Attendee
    {
        public string Initial => string.IsNullOrWhiteSpace(Name) ? "?" : Name[0].ToString().ToUpper();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public Color StatusColor =>
            Status switch
            {
                "Confirmed" => Color.FromArgb("#4ADE80"),
                "Pending" => Color.FromArgb("#FBBF24"),
                "Declined" => Color.FromArgb("#F87171"),
                _ => Color.FromArgb("#9CA3AF")
            };

        public Attendee() { }

        public Attendee(EventAttendeeDto dto)
        {
            Debug.WriteLine($"MAPPING: {dto.fullName}");

            Name = dto.fullName;
            Email = dto.email;
            Status = dto.status;
        }
    }
}
