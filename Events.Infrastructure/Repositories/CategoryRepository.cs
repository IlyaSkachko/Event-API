

using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories.Interfaces;

namespace Events.Infrastructure.Repositories
{
    public class CategoryRepository : GeneralRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
