using Events.Application.DTO.Participant;

namespace Events.Application.Interfaces.UseCase.Token
{
    public interface ITokenGenerateUseCase
    {
        string Execute(ParticipantDTO participant, DateTime expiresTime);
    }
}
