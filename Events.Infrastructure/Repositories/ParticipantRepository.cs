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

        public async Task<Participant> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await table.Where(participant => participant.Email.Equals(email)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Participant> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await dbContext.Participants.FirstOrDefaultAsync(participant => participant.RefreshToken.Equals(refreshToken), cancellationToken);
        }
    }
}
