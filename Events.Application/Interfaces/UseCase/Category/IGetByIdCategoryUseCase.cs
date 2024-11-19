using Events.Application.DTO.Category;

namespace Events.Application.Interfaces.UseCase.Category
{
    public interface IGetByIdCategoryUseCase
    {
        Task<CategoryDTO> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
