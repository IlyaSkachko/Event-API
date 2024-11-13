using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration.Entities
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable(nameof(Participant)).HasKey(participant => participant.Id);

            builder.Property(participant => participant.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(participant => participant.Surname)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(participant => participant.Password)
                .IsRequired();
            builder.Property(participant => participant.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(participant => participant.RegistrationDate)
                .IsRequired();
            builder.Property(participant => participant.BirthDate)
                .IsRequired();
            builder.Property(participant => participant.RefreshToken);
            builder.Property(participant => participant.Role)
                .IsRequired()
                .HasConversion<string>();

            builder.HasMany(participant => participant.EventParticipants)
                .WithOne(_event => _event.Participant)
                .HasForeignKey(_event => _event.ParticipantId);
        }
    }
}
