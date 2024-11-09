using Events.Application.DTO.Participant;
using Events.Domain.Models;

namespace Events.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(ParticipantDTO participant, DateTime expiresTime);
        string GenerateRefreshToken(ParticipantDTO participant, DateTime expiresTime);
    }
}
