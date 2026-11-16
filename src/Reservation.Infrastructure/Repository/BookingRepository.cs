using Reservation.Application.Abstractions.Repository;
using Reservation.Domain.Entity;
using Reservation.Infrastructure.Data;

namespace Reservation.Infrastructure.Repository
{
    internal class BookingRepository : IBookingRepository
    {
        private readonly ApiDbContext _dbContext;

        public BookingRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Booking entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(Booking entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
