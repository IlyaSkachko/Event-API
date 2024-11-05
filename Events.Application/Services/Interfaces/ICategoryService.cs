using Events.Application.DTO.Category;

namespace Events.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDTO>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<CategoryDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(CategoryDTO dto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task InsertAsync(CategoryDTO dto, CancellationToken cancellationToken);
    }
}
