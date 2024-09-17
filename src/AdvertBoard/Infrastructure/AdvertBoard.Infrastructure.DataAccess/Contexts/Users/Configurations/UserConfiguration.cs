using AdvertBoard.Domain.Contexts.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Configurations;

/// <summary>
/// Конфигурация типа <see cref="User"/>.
/// </summary>
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .HasMaxLength(25)
            .IsRequired();

        builder
            .Property(x => x.Lastname)
            .HasMaxLength(25)
            .IsRequired(false);

        builder
            .Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(x => x.Phone)
            .HasMaxLength(11)
            .IsRequired();

        builder
            .Property(x => x.Rating)
            .IsRequired(false);
        
        builder
            .Property(x => x.CreatedAt)
            .HasConversion(d => d, d => DateTime.SpecifyKind(d, DateTimeKind.Utc))
            .IsRequired();
        
        // Конфигурация отношений.
        // Объявления.
        builder
            .HasMany(x => x.Adverts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        
        // Оставленные отзывы.
        builder
            .HasMany(x => x.LeftReviews)
            .WithOne(x => x.OwnerUser)
            .HasForeignKey(x => x.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        
        // Полученнные отзывы.
        builder
            .HasMany(x => x.ReceivedReviews)
            .WithOne(x => x.ReceiverUser)
            .HasForeignKey(x => x.ReceiverUserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        // Комментарии.
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}