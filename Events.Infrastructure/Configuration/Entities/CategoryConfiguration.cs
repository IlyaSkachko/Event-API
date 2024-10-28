
using Events.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infrastructure.Configuration.Entities
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public static void Initialization(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Category>().HasData(
                  new Category { Id = 1, Name = "Conference" },
                  new Category { Id = 2, Name = "Webinar" },
                  new Category { Id = 3, Name = "Concert" }
            );
        }
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category)).HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(c => c.Events)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}
