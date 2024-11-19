namespace Events.Application.Interfaces.UseCase.Category
{
    public interface IDeleteCategoryUseCase
    {
        Task ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
