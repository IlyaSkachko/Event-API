using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories;
using Events.Domain.Interfaces.UOW;
using Events.Domain.Interfaces.Repositories;

namespace Events.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICategoryRepository categoryRepository;
        private IEventParticipantRepository eventParticipantRepository;
        private IEventRepository eventRepository;
        private IParticipantRepository participantRepository;
        private ApplicationDbContext dbContext;

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository is null)
                {
                    categoryRepository = new CategoryRepository(dbContext);
                }

                return categoryRepository;
            }
        }

        public IEventRepository EventRepository
        {
            get
            {
                if (eventRepository is null)
                {
                    eventRepository = new EventRepository(dbContext);
                }

                return eventRepository;
            }
        }

        public IEventParticipantRepository EventParticipantRepository
        {
            get
            {
                if (eventParticipantRepository is null)
                {
                    eventParticipantRepository = new EventParticipantRepository(dbContext);
                }
                return eventParticipantRepository;
            }
        }

        public IParticipantRepository ParticipantRepository
        {
            get
            {
                if (participantRepository is null)
                {
                    participantRepository = new ParticipantRepository(dbContext);
                }
                return participantRepository;
            }
        }


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
           await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
