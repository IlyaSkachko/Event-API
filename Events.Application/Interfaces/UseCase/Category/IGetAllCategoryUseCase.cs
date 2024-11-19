using Events.Application.DTO.Category;

namespace Events.Application.Interfaces.UseCase.Category
{
    public interface IGetAllCategoryUseCase
    {
        Task<IEnumerable<CategoryDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
