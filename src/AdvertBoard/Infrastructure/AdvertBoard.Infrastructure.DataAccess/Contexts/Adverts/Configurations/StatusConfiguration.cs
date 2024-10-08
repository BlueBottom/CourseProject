using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Contexts.Adverts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertBoard.Infrastructure.DataAccess.Contexts.Adverts.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        // Конфигурация свойств.
        builder.HasKey(x => x.Id);

        builder
            .Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name).IsRequired();

        builder.HasData(
            Enum.GetValues(typeof(AdvertStatus))
                .OfType<AdvertStatus>()
                .Select(x => new Status()
                {
                    Id = x, 
                    Name = x.ToString()
                })
                .ToArray());
    }
}