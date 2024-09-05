using AdvertBoard.Domain.Contexts.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Categories.Configurations;

/// <summary>
/// Конфигурация типа <see cref="Category"/>.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .HasMaxLength(25)
            .IsRequired();
        
        builder
            .Property(x => x.CreatedAt)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        // Конфигурация отношений.
        // Объявления.
        builder
            .HasMany(x => x.Adverts)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        // Категории.      
        builder
            .HasOne(x => x.Parent)
            .WithMany(x => x.Childs)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}