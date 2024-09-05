using AdvertBoard.Domain.Contexts.Images;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Images.Configurations;

/// <summary>
/// Конфигурация типа <see cref="Image"/>.
/// </summary>
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Content)
            .IsRequired();

        builder
            .Property(x => x.Title)
            .HasMaxLength(20)
            .IsRequired(false);
        
        builder
            .Property(x => x.CreatedAt)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        // Конфигурация отношений.
        // Объявление.
        builder
            .HasOne(x => x.Advert)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}