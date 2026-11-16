using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Entity;
using Reservation.Infrastructure.Identity.Entity;

namespace Reservation.Infrastructure.Data
{
    public class ApiDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
