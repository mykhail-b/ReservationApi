using Microsoft.AspNetCore.Identity;
using Reservation.Application.Abstractions.Service;
using Reservation.Application.Models.Auth;
using Reservation.Infrastructure.Identity.Entity;

namespace Reservation.Infrastructure.Services
{
    internal sealed class AuthService : IAuthService
    {
        private const string CustomerRole = "Customer";

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return await CreateAuthResponseAsync(user);
        }

        public async Task<AuthResponse> RegisterUserAsync(RegisterRequest request)
        {
            await EnsureCustomerRoleExistsAsync();

            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Fullname = request.UserName
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(FormatIdentityErrors(createResult));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, CustomerRole);
            if (!roleResult.Succeeded)
            {
                var cleanupResult = await _userManager.DeleteAsync(user);
                var error = $"Could not assign the Customer role: {FormatIdentityErrors(roleResult)}";

                if (!cleanupResult.Succeeded)
                {
                    error += $" Could not remove the newly created user: {FormatIdentityErrors(cleanupResult)}";
                }

                throw new InvalidOperationException(error);
            }

            return await CreateAuthResponseAsync(user);
        }

        private async Task EnsureCustomerRoleExistsAsync()
        {
            if (await _roleManager.RoleExistsAsync(CustomerRole))
            {
                return;
            }

            var result = await _roleManager.CreateAsync(new AppRole { Name = CustomerRole });
            if (!result.Succeeded && !await _roleManager.RoleExistsAsync(CustomerRole))
            {
                throw new InvalidOperationException(FormatIdentityErrors(result));
            }
        }

        private async Task<AuthResponse> CreateAuthResponseAsync(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return _tokenService.CreateToken(user.Id, user.Email!, roles);
        }

        private static string FormatIdentityErrors(IdentityResult result)
        {
            return string.Join("; ", result.Errors.Select(error => error.Description));
        }
    }
}
