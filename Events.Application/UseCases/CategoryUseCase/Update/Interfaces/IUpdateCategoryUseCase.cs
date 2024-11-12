using Events.Application.DTO.Category;

namespace Events.Application.UseCases.CategoryUseCase.Update.Interfaces
{
    public interface IUpdateCategoryUseCase
    {
        Task ExecuteAsync(CategoryDTO dto, CancellationToken cancellationToken);
    }
}
