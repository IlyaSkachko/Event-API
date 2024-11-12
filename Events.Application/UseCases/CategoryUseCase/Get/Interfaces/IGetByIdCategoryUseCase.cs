using Events.Application.DTO.Category;

namespace Events.Application.UseCases.CategoryUseCase.Get.Interfaces
{
    public interface IGetByIdCategoryUseCase
    {
        Task<CategoryDTO> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
