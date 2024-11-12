using Events.Application.DTO.Category;

namespace Events.Application.UseCases.CategoryUseCase.Insert.Interfaces
{
    public interface IInsertCategoryUseCase
    {
        Task ExecuteAsync(CreateCategoryDTO dto, CancellationToken cancellationToken);
    }
}
