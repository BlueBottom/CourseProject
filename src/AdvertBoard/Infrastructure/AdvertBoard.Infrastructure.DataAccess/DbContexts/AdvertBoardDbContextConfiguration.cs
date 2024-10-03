using AdvertBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdvertBoard.Infrastructure.DataAccess.DbContexts;

/// <summary>
/// Конфигурация <see cref="AdvertBoardDbContext"/> контекста 
/// </summary>
public class AdvertBoardDbContextConfiguration : IDbContextOptionsConfigurator<AdvertBoardDbContext>
{
    private const string PostgresConnectionStringName = "DefaultConnection";
    
    private readonly IConfiguration _configuration;
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// Конструктор <see cref="AdvertBoardDbContextConfiguration"/>
    /// </summary>
    /// <param name="configuration">Конфигурации</param>
    /// <param name="loggerFactory">Фабрика логгеров</param>
    public AdvertBoardDbContextConfiguration(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _loggerFactory = loggerFactory;
    }
    
    /// <inheritdoc />
    /// <exception cref="InvalidOperationException">Строка подключения не найдена в конфигурациях</exception>
    public void Configure(DbContextOptionsBuilder<AdvertBoardDbContext> options)
    {
        var connectionString = _configuration.GetConnectionString(PostgresConnectionStringName);
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Не найдена строка подключения с именем '{PostgresConnectionStringName}'");
        options.UseNpgsql(connectionString);
        options.UseLoggerFactory(_loggerFactory);
        options.UseLazyLoadingProxies();
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
}