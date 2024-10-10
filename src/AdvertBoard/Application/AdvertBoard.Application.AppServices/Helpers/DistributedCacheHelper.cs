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
    /// <typeparam name="T">Сущность.</typeparam>
    /// <returns>Сущность.</returns>
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
    /// <param name="cacheLifetimeInHours">Время жизни сущности в кэше в часах.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <typeparam name="T">Сущность.</typeparam>
    public static async Task PutByKeyAsync<T>(this IDistributedCache distributedCache,
        string key,
        T entity,
        int cacheLifetimeInHours,
        CancellationToken cancellationToken)
    {
        var itemToCache = JsonSerializer.Serialize(entity);
        await distributedCache.SetStringAsync(key, itemToCache,
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(cacheLifetimeInHours)
            },
            cancellationToken);
    }
}