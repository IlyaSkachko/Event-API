

using Events.Infrastructure.Repositories;

namespace Events.Infrastructure.UOW.Interfaces
{
    public interface IUnitOfWork
    {
        CategoryRepository CategoryRepository { get; }
        EventRepository EventRepository { get; }
        EventParticipantRepository EventParticipantRepository { get; }
        ParticipantRepository ParticipantRepository { get; }
    }
}
