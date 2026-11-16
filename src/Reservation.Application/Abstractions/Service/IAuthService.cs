using Reservation.Application.Models.Auth;

namespace Reservation.Application.Abstractions.Service
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUserAsync(RegisterRequest request);

        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
