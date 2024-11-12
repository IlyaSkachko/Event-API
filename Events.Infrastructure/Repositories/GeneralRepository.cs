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
            var entityId = entity.GetType().GetProperty("Id").GetValue(entity, null);

            bool exists = await table.AnyAsync(entity => EF.Property<int>(entity, "Id") == (int)entityId, cancellationToken);

            if (!exists)
            {
                throw new InvalidOperationException("Invalid delete operation! Entity not found!");
            }

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
            if (pageNumber < 1 || pageSize < 1)
            {
                throw new ArgumentException("Negative page options");
            }

            var collection = await table.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return collection;
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await table.FindAsync(id, cancellationToken);

            return entity is null ? throw new InvalidOperationException("Entity not found") : entity;
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            var entityId = entity.GetType().GetProperty("Id").GetValue(entity, null);

            bool exists = await table.AnyAsync(entity => EF.Property<int>(entity, "Id") == (int)entityId, cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException("Invalid insert operation! Entity already exist!");
            }

            await table.AddAsync(entity, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var entityId = entity.GetType().GetProperty("Id").GetValue(entity, null);

            bool exists = await table.AnyAsync(entity => EF.Property<int>(entity, "Id") == (int)entityId, cancellationToken);

            if (!exists)
            {
                throw new InvalidOperationException("Invalid update operation! Entity not found!");
            }

            table.Update(entity);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
