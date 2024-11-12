namespace Events.Application.UseCases.HashUseCase.Verify.Interfaces
{
    public interface IVerifyPasswordUseCase
    {
        bool Execute(string password, string hash, byte[] salt);
    }
}
