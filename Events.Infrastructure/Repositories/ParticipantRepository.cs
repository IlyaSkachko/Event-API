using Events.Domain.Models;
using Events.Infrastructure.Data;
using Events.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Repositories
{
    public class ParticipantRepository : GeneralRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
