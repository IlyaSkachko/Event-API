using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories
{
    public class EventRepository : GeneralRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddImageAsync(int id, byte[] image, CancellationToken cancellationToken)
        {
            var e = await GetByIdAsync(id, cancellationToken);  
            e.Image = image;
            await UpdateAsync(e, cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.CategoryId == categoryId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByCategoryAsync(int pageNumber, int pageSize, int categoryId, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.CategoryId == categoryId).Skip((pageNumber - 1) * pageSize).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByDateAsync(DateTime dateTime, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.EventDate.Equals(dateTime)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByDateAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.EventDate.Equals(dateTime)).Skip((pageNumber - 1) * pageSize).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByLocationAsync(string location, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.Location.Equals(location)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByLocationAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.Location.Equals(location)).Skip((pageNumber - 1) * pageSize).ToListAsync(cancellationToken);
        }

        public async Task<Event> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(e => e.Name.Equals(name)).FirstAsync(cancellationToken);
        }
    }
}
