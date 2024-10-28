

using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories.Interfaces;

namespace Events.Infrastructure.Repositories
{
    public class EventRepository : GeneralRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
