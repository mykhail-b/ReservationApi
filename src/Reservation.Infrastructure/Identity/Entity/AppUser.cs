using Microsoft.AspNetCore.Identity;

namespace Reservation.Infrastructure.Identity.Entity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Fullname { get; set; }
    }
}
