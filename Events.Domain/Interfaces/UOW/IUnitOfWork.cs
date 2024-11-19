using Events.Domain.Interfaces.Repositories;

namespace Events.Domain.Interfaces.UOW
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IEventRepository EventRepository { get; }
        IEventParticipantRepository EventParticipantRepository { get; }
        IParticipantRepository ParticipantRepository { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}