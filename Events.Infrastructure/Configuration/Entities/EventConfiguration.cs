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

            builder.Property(e => e.Id)
                .IsRequired();
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(e => e.EventDate)
                .IsRequired();
            builder.Property(e => e.Location)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(e => e.CategoryId)
                .IsRequired();
            builder.Property(e => e.MaxParticipants)
                .IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId);
            builder.HasMany(e => e.EventParticipants)
                .WithOne(ep => ep.Event)
                .HasForeignKey(ep => ep.EventId);
        }
    }
}
