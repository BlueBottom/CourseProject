using AdvertBoard.Domain.Contexts.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Reviews.Configurations;

/// <summary>
/// Конфигурация типа <see cref="Review"/>.
/// </summary>
public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .HasMaxLength(400)
            .IsRequired();
        
        builder
            .Property(x => x.CreatedAt)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        // Конфигурация отношений.
        // Владелец отзыва.
        builder
            .HasOne(x => x.OwnerUser)
            .WithMany(x => x.LeftReviews)
            .HasForeignKey(x => x.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        // Получатель отзыва.
        builder
            .HasOne(x => x.ReceiverUser)
            .WithMany(x => x.ReceivedReviews)
            .HasForeignKey(x => x.ReceiverUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}