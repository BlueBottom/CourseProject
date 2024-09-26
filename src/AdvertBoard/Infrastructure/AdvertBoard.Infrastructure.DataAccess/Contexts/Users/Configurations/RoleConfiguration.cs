using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Contexts.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Users.Configurations;

/// <summary>
/// Конфигурация типа <see cref="Role"/>.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder
            .Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).IsRequired();

        builder.HasData(
            Enum.GetValues(typeof(UserRole))
            .OfType<UserRole>()
            .Select(x => new Role
            {
                Id = x, 
                Name = x.ToString()
            })
            .ToArray());
    }
}