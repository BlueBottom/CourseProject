using AdvertBoard.Infrastructure.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Hosts.DbMigrator.DbContext;

/// <summary>
/// Контекст БД для миграций.
/// </summary>
public class MigratorDbContext : AdvertBoardDbContext
{
    /// <summary>
    /// Инициализирует экземпляр класса.
    /// </summary>
    /// <param name="options">Параметры контекста.</param>
    public MigratorDbContext(DbContextOptions options) : base(options)
    {
    }
}