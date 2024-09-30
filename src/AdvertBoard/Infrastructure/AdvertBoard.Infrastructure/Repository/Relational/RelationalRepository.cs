using Microsoft.EntityFrameworkCore;

namespace AdvertBoard.Infrastructure.Repository.Relational;

/// <inheritdoc cref="AdvertBoard.Infrastructure.Repository.Relational.IRelationalRepository{TEntity}"/>
public class RelationalRepository<TEntity> : Repository<TEntity>, IRelationalRepository<TEntity> 
    where TEntity : class
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="RelationalRepository{TEntity}"/>.
    /// </summary>
    public RelationalRepository(DbContext dbContext) : base(dbContext)
    {
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> GetBySql(string sql, params object[] parameters)
    {
        return DbSet.FromSqlRaw(sql, parameters);
    }
}