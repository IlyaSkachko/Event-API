using CloudinaryDotNet;
using Events.Application.DTO.Category;
using Events.Application.DTO.Event;
using Events.Domain.Models;

namespace Events.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByDateAsync(DateTime dateTime, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByLocationAsync(string location, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByCategoryAsync(CategoryDTO categoryDTO, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByDateAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByLocationAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken);
        Task<IEnumerable<EventDTO>> GetByCategoryAsync(int pageNumber, int pageSize, int id, CancellationToken cancellationToken);
        Task<EventDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<EventDTO> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task AddAsync(EventDTO dto, CancellationToken cancellationToken);
        Task UpdateAsync(EventDTO dto, CancellationToken cancellationToken);
        Task AddImageAsync(int eventId, string url, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
