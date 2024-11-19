using Events.Application.DTO.Category;

namespace Events.Application.Interfaces.UseCase.Category
{
    public interface IUpdateCategoryUseCase
    {
        Task ExecuteAsync(CategoryDTO categoryDTO, CancellationToken cancellationToken);
    }
}
