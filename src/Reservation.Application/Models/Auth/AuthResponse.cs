using System;
using System.Collections.Generic;
using System.Text;

namespace Reservation.Application.Models.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}
