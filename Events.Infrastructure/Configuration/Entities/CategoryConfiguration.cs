
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
            builder.ToTable(nameof(Category)).HasKey(category => category.Id);

            builder.Property(category => category.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(category => category.Events)
                .WithOne(_event => _event.Category)
                .HasForeignKey(_event => _event.CategoryId);
        }
    }
}
