using Events.Application.DTO.Participant;

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
        Task<string> Login(ParticipantAuthDTO dto, CancellationToken cancellationToken);
    }
}
