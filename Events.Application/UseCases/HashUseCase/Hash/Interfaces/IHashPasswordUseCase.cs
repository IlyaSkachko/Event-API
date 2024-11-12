namespace Events.Application.UseCases.HashUseCase.Hash.Interfaces
{
    public interface IHashPasswordUseCase
    {
        string Execute(string password, out byte[] salt);
    }
}
