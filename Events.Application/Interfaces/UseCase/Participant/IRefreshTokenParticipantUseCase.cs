using Microsoft.AspNetCore.Http;

namespace Events.Application.Interfaces.UseCase.Participant
{
    public interface IRefreshTokenParticipantUseCase
    {
        Task ExecuteAsync(HttpContext httpContext, CancellationToken cancellationToken);
    }
}
