using Events.Domain.Models;

namespace Events.Domain.Interfaces.Repositories
{
    public interface IParticipantRepository : IGeneralRepository<Participant>
    {
        Task<Participant> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Participant> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
