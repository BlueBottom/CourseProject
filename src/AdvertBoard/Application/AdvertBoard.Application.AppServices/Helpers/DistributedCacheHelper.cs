using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Вспомогательный класс для работы с распределенным кэшем.
/// </summary>
public static class DistributedCacheHelper
{
    /// <summary>
    /// Получает сущность из кэша.
    /// </summary>
    /// <param name="distributedCache">Распределенный кэш.</param>
    /// <param name="key">Ключ.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <typeparam name="T">Тип кэшируемого объекта.</typeparam>
    /// <returns>Значение из кэша по ключу.</returns>
    public static async Task<T?> GetByKeyAsync<T>(this IDistributedCache distributedCache, string key,
        CancellationToken cancellationToken)
    {
        var cachedItem = await distributedCache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrWhiteSpace(cachedItem)) return default;
        var result = JsonSerializer.Deserialize<T>(cachedItem);
        return result;
    }

    /// <summary>
    /// Помещает сущность в кэш.
    /// </summary>
    /// <param name="distributedCache">Распределенный кэш.</param>
    /// <param name="key">Ключ.</param>
    /// <param name="entity">Сущность, помещемая в кэш.</param>
    /// <param name="cacheLifetimeInMinutes">Время жизни в минутах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <typeparam name="T">Тип кэшируемого объекта.</typeparam>
    public static async Task PutByKeyAsync<T>(this IDistributedCache distributedCache,
        string key,
        T entity,
        int cacheLifetimeInMinutes,
        CancellationToken cancellationToken)
    {
        var itemToCache = JsonSerializer.Serialize(entity);
        await distributedCache.SetStringAsync(key, itemToCache,
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(cacheLifetimeInMinutes)
            },
            cancellationToken);
    }
}