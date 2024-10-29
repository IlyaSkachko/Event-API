using Events.Application.DTO.Category;
using Events.Application.DTO.Event;

namespace Events.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByDateAsync(DateTime date, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByLocation(string location, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByCategory(CategoryDTO categoryDTO, CancellationToken cancellationToken);
        Task<EventDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<EventDTO> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task AddAsync(EventDTO dto, CancellationToken cancellationToken);
        Task UpdateAsync(EventDTO dto, CancellationToken cancellationToken);
        Task AddImageAsync(EventImageDTO dto, CancellationToken cancellationToken);
        Task DeleteAsync(EventDTO dto, CancellationToken cancellationToken);
    }
}
