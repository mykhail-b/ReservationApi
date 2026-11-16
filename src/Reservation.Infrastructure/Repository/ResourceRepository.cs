using Reservation.Application.Abstractions.Repository;
using Reservation.Domain.Entity;
using Reservation.Infrastructure.Data;

namespace Reservation.Infrastructure.Repository
{
    internal class ResourceRepository : IResourceRepository
    {
        private readonly ApiDbContext _dbContext;

        public ResourceRepository(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Resource entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Resource?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(Resource entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Update(Resource entity)
        {
            throw new NotImplementedException();
        }
    }
}
