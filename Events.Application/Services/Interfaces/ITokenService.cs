using Events.Domain.Models;

namespace Events.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string Generate(Participant participant);
    }
}
