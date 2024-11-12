using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Events.Infrastructure.Configuration.Entities
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {   
            builder.ToTable(nameof(Event)).HasKey(e => e.Id);

            builder.Property(_event => _event.Id)
                .IsRequired();
            builder.Property(_event => _event.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(_event => _event.Description)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(_event => _event.EventDate)
                .IsRequired();
            builder.Property(_event => _event.Location)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(_event => _event.CategoryId)
                .IsRequired();
            builder.Property(_event => _event.MaxParticipants)
                .IsRequired();

            builder.HasOne(_event => _event.Category)
                .WithMany(category => category.Events)
                .HasForeignKey(_event => _event.CategoryId);
            builder.HasMany(_event => _event.EventParticipants)
                .WithOne(eventParticipant => eventParticipant.Event)
                .HasForeignKey(eventParticipant => eventParticipant.EventId);
        }
    }
}
