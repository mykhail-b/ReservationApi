using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Reservation.Application.Abstractions.Service;
using Reservation.Application.Models.Auth;
using Reservation.Infrastructure.Configuration;

namespace Reservation.Infrastructure.Services
{
    internal sealed class TokenService : ITokenService
    {
        private readonly JwtSettings _settings;

        public TokenService(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;

            if (Encoding.UTF8.GetByteCount(_settings.Key) < 32)
            {
                throw new InvalidOperationException("Jwt:Key must contain at least 32 bytes for HS256.");
            }

            if (string.IsNullOrWhiteSpace(_settings.Issuer)
                || string.IsNullOrWhiteSpace(_settings.Audience)
                || _settings.ExpirationMinutes <= 0)
            {
                throw new InvalidOperationException(
                    "Jwt:Issuer, Jwt:Audience, and a positive Jwt:ExpirationMinutes must be configured.");
            }
        }

        public AuthResponse CreateToken(Guid userId, string email, IEnumerable<string> roles)
        {
            var now = DateTime.UtcNow;
            var expiresAt = now.AddMinutes(_settings.ExpirationMinutes);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.Email, email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: expiresAt,
                signingCredentials: credentials);

            return new AuthResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = expiresAt
            };
        }
    }
}
