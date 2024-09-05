using AdvertBoard.Infrastructure.DataAccess.DbContexts;
using AdvertBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertBoard.Infrastructure.ComponentRegistrar;

/// <summary>
/// Регистратор компонентов.
/// </summary>
public static class ContextRegistrar
{
    /// <summary>
    /// Добавляет механизм подключения к БД в DI.
    /// </summary>
    /// <param name="serviceCollection">IoC.</param>
    /// <param name="configuration">Конфигурация.</param>
    /// <returns>IoC.</returns>
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IDbContextOptionsConfigurator<AdvertBoardDbContext>, AdvertBoardDbContextConfiguration>();
        serviceCollection.AddDbContext<AdvertBoardDbContext>((sp, options) =>
            sp.GetRequiredService<IDbContextOptionsConfigurator<AdvertBoardDbContext>>()
                .Configure((DbContextOptionsBuilder<AdvertBoardDbContext>)options)
            );
        return serviceCollection;
    }
}