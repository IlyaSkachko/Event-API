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

        public async Task AddImageAsync(int id, string image, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken); 

            if (entity is null)
            {
                throw new InvalidOperationException("Invalid add image operation! Event does not exist");
            }    
            
            entity.Image = image;

            await UpdateAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.CategoryId == categoryId).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByCategoryAsync(int pageNumber, int pageSize, int categoryId, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.CategoryId == categoryId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByDateAsync(DateTime dateTime, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.EventDate.Equals(dateTime)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByDateAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.EventDate.Equals(dateTime)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByLocationAsync(string location, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.Location.Equals(location)).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Event>> GetByLocationAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.Location.Equals(location)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<Event> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await table.AsNoTracking().Where(entity => entity.Name.Equals(name)).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
