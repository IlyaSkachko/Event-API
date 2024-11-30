using Events.Application.DTO.Participant;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface ILoginParticipantUseCase
    {
        Task ExecuteAsync(HttpContext httpContext, ParticipantAuthDTO dto, CancellationToken cancellationToken);
    }
}
