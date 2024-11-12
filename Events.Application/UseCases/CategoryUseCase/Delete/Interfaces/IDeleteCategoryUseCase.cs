namespace Events.Application.UseCases.CategoryUseCase.Delete.Interfaces
{
    public interface IDeleteCategoryUseCase
    {
        Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
