using Events.Application.DTO.Participant;
using Events.Application.DTO.Token;

namespace Events.Application.Services.Interfaces
{
    public interface IParticipantService
    {
        Task<IEnumerable<ParticipantDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ParticipantDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<ParticipantDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateParticipantDTO dto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task InsertAsync(CreateParticipantDTO dto, CancellationToken cancellationToken);
        Task<TokenDTO> Login(ParticipantAuthDTO dto, CancellationToken cancellationToken);
        Task<ParticipantDTO> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task DeleteRefreshTokenAsync(ParticipantDTO dto, CancellationToken cancellationToken);
    }
}
