

using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration.Entities
{
    public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
    {
        public void Configure(EntityTypeBuilder<EventParticipant> builder)
        {
            builder.HasKey(eventParticipant => new { eventParticipant.EventId, eventParticipant.ParticipantId });

            builder.HasOne(eventParticipant => eventParticipant.Event)
                   .WithMany(_event => _event.EventParticipants)
                   .HasForeignKey(eventParticipant => eventParticipant.EventId);

            builder.HasOne(eventParticipant => eventParticipant.Participant)
                   .WithMany(participant => participant.EventParticipants)
                   .HasForeignKey(eventParticipant => eventParticipant.ParticipantId);
        }
    }
}
