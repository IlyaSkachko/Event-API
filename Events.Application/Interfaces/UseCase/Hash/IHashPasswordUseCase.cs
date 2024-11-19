namespace Events.Application.Interfaces.UseCase.Hash
{
    public interface IHashPasswordUseCase
    {
        string Execute(string password, out byte[] salt);
    }
}
