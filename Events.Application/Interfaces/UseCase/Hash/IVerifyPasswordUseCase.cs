namespace Events.Application.Interfaces.UseCase.Hash
{
    public interface IVerifyPasswordUseCase
    {
        bool Execute(string password, string hash, byte[] salt);
    }
}
