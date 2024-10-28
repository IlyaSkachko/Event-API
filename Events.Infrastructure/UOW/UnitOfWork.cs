using Events.Infrastructure.Data;
using Events.Infrastructure.Repositories;
using Events.Infrastructure.UOW.Interfaces;


namespace Events.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private CategoryRepository categoryRepository;
        private EventParticipantRepository eventParticipantRepository;
        private EventRepository eventRepository;
        private ParticipantRepository participantRepository;


        public CategoryRepository CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new CategoryRepository(dbContext);
                }
                return categoryRepository;
            }
        }

        public EventRepository EventRepository
        {
            get
            {
                if (eventRepository == null)
                {
                    eventRepository = new EventRepository(dbContext);
                }
                return eventRepository;
            }
        }

        public EventParticipantRepository EventParticipantRepository
        {
            get
            {
                if (eventParticipantRepository == null)
                {
                    eventParticipantRepository = new EventParticipantRepository(dbContext);
                }
                return eventParticipantRepository;
            }
        }

        public ParticipantRepository ParticipantRepository
        {
            get
            {
                if (participantRepository == null)
                {
                    participantRepository = new ParticipantRepository(dbContext);
                }
                return participantRepository;
            }
        }


        private ApplicationDbContext dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
