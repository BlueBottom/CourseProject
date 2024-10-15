using System.Reflection;

namespace AdvertBoard.Infrastructure.DataAccess.Helpers;

/// <summary>
/// Класс для работы с встроенными ресурсами.
/// </summary>
public static class EmbeddedResourceHelper
{
    /// <summary>
    /// Получает содержимое ресурса.
    /// </summary>
    /// <param name="fullResourceName">Путь к ресурсу.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Содержимое ресурма в виде <see cref="string"/>.</returns>
    public static async Task<string> GetEmbeddedResourceAsString(string fullResourceName, CancellationToken cancellationToken)
    {
        var resourceBodyStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
            $"{Assembly.GetExecutingAssembly().GetName().Name}.{fullResourceName}")!;
        using var resourceBodyStreamReader = new StreamReader(resourceBodyStream);
        var resourceBody = await resourceBodyStreamReader.ReadToEndAsync(cancellationToken);
        return resourceBody;
    }
}