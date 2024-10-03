namespace AdvertBoard.Infrastructure.Repository.Relational;

/// <summary>
/// Репозиторий для работы с реляционными базами данных.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRelationalRepository<TEntity> : IRepository<TEntity> 
    where TEntity : class
{
    /// <summary>
    /// Получить объекты <see cref="TEntity"/> из БД через SQL запрос.
    /// </summary>
    /// <param name="sql">Запрос SQL.</param>
    /// <param name="parameters"></param>
    /// <returns>Нематериализованный запрос.</returns>
    IQueryable<TEntity> GetBySql(string sql, params object[] parameters);
}