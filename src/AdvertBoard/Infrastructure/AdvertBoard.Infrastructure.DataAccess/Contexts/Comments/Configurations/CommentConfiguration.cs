using AdvertBoard.Domain.Contexts.Comments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Comments.Configurations;

/// <summary>
/// Конфигурация типа <see cref="Comment"/>.
/// </summary>
public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Content)
            .HasMaxLength(400)
            .IsRequired();
        
        builder
            .Property(x => x.CreatedAt)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        // Конфигурация отношений.
        // Объявление.
        builder
            .HasOne(x => x.Advert)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Пользователь.
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}