using Events.Domain.Models;
using Events.Infrastructure.Configuration.Entities;
using Microsoft.EntityFrameworkCore;


namespace Events.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventsParticipant { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventParticipantConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ParticipantConfiguration).Assembly);

            CategoryConfiguration.Initialization(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
