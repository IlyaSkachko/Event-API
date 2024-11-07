using Events.Domain.Models;

namespace Events.Domain.Interfaces.Repositories
{
    public interface IEventRepository : IGeneralRepository<Event>
    {
        Task<Event> GetByNameAsync(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Event>> GetByDateAsync(DateTime dateTime, CancellationToken cancellationToken);
        Task<IEnumerable<Event>> GetByLocationAsync(string location, CancellationToken cancellationToken);
        Task<IEnumerable<Event>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken);
        Task<IEnumerable<Event>> GetByDateAsync(int pageNumber, int pageSize, DateTime dateTime, CancellationToken cancellationToken);
        Task<IEnumerable<Event>> GetByLocationAsync(int pageNumber, int pageSize, string location, CancellationToken cancellationToken);
        Task<IEnumerable<Event>> GetByCategoryAsync(int pageNumber, int pageSize, int categoryId, CancellationToken cancellationToken);
        Task AddImageAsync(int id, string image, CancellationToken cancellationToken);
    }
}
