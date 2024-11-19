using Events.Infrastructure.Data;
using Events.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        protected ApplicationDbContext dbContext;
        protected DbSet<T> table;

        public GeneralRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = this.dbContext.Set<T>();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collection = await table.AsNoTracking().ToListAsync(cancellationToken);

            return collection;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await table.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return collection;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await table.FindAsync(id, cancellationToken);

            return entity;
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await table.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            table.Update(entity);
        }
    }
}
