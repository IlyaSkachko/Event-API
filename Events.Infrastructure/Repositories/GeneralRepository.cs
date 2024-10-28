

using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        ApplicationDbContext dbContext;
        DbSet<T> table;
        public GeneralRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            table = this.dbContext.Set<T>();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            table.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            var collection = await table.AsNoTracking().ToListAsync(cancellationToken);
            return collection;
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var collection = await table.AsNoTracking().Skip((pageNumber - 1) * pageSize).ToListAsync(cancellationToken);
            return collection;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await table.FindAsync(id, cancellationToken);
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await table.AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            table.Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
