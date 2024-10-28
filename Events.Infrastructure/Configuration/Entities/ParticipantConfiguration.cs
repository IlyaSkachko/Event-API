

using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration.Entities
{
    public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
    {
        public void Configure(EntityTypeBuilder<Participant> builder)
        {
            builder.ToTable(nameof(Participant)).HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Surname)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Password)
                .IsRequired();
            builder.Property(p => p.PasswordSalt)
                .IsRequired();
            builder.Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.RegistrationDate)
                .IsRequired();
            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.HasMany(p => p.EventParticipants)
                .WithOne(e => e.Participant)
                .HasForeignKey(e => e.ParticipantId);
        }
    }
}
