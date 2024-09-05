using AdvertBoard.Hosts.DbMigrator.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Hosts.DbMigrator;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (connectionString is null) throw new ArgumentNullException(nameof(connectionString));
        builder.Services.AddDbContext<MigratorDbContext>(options => 
            options.UseNpgsql(connectionString));

        var host = builder.Build();

        using var scope = host.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MigratorDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}