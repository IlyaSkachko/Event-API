using Events.Application.DTO.Category;

namespace Events.Application.Interfaces.UseCase.Category
{
    public interface IInsertCategoryUseCase
    {
        Task ExecuteAsync(CreateCategoryDTO dto, CancellationToken cancellationToken);
    }
}
