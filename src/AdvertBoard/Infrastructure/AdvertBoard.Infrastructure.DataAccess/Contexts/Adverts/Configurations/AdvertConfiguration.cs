using AdvertBoard.Domain.Contexts.Adverts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Adverts.Configurations;

/// <summary>
/// Конфигурация типа <see cref="Advert"/>.
/// </summary>
public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Advert> builder)
    {
        //Конфигурация свойств.
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(80);
        
        builder
            .Property(x => x.Description)
            .HasMaxLength(1000)
            .IsRequired(false);
        
        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .Property(x => x.Status)
            .IsRequired();

        builder
            .Property(x => x.Location)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(11)
            .IsRequired(false);
        
        builder
            .Property(x => x.CreatedAt)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
            
        // Конфигурация отношений.
        // Комментарии.
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.Advert)
            .HasForeignKey(x => x.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Изображения.
        builder
            .HasMany(x => x.Images)
            .WithOne(x => x.Advert)
            .HasForeignKey(x => x.AdvertId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Пользователь.
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Adverts)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        // Категория.
        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.Adverts)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}