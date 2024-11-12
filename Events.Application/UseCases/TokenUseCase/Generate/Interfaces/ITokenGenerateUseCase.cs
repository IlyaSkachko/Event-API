using Events.Application.DTO.Participant;

namespace Events.Application.UseCases.TokenUseCase.Generate.Interfaces
{
    public interface ITokenGenerateUseCase
    {
        string Execute(ParticipantDTO participant, DateTime expiresTime);
    }
}
