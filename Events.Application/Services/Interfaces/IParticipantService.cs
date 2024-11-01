using Events.Application.DTO.Participant;

namespace Events.Application.Services.Interfaces
{
    public interface IParticipantService
    {
        Task<IEnumerable<ParticipantDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ParticipantDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<ParticipantDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(ParticipantDTO dto, CancellationToken cancellationToken);
        Task DeleteAsync(ParticipantDTO dto, CancellationToken cancellationToken);
        Task InsertAsync(ParticipantDTO dto, CancellationToken cancellationToken);
    }
}
