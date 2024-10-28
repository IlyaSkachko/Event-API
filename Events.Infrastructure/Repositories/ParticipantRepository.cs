

using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories.Interfaces;

namespace Events.Infrastructure.Repositories
{
    public class ParticipantRepository : GeneralRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
