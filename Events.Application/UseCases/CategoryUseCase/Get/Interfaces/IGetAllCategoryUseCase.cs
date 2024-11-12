using Events.Application.DTO.Category;

namespace Events.Application.UseCases.CategoryUseCase.Get.Interfaces
{
    public interface IGetAllCategoryUseCase
    {
        Task<IEnumerable<CategoryDTO>> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
