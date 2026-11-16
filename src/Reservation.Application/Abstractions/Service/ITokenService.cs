using Reservation.Application.Models.Auth;

namespace Reservation.Application.Abstractions.Service
{
    public interface ITokenService
    {
        AuthResponse CreateToken(Guid userId, string email, IEnumerable<string> roles);
    }
}
