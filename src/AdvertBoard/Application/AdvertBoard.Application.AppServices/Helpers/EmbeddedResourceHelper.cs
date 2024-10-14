using System.Reflection;

namespace AdvertBoard.Application.AppServices.Helpers;

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
        var mailBodyStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
            $"{Assembly.GetExecutingAssembly().GetName().Name}.{fullResourceName}")!;
        using var mailBodyStreamReader = new StreamReader(mailBodyStream);
        var mailBody = await mailBodyStreamReader.ReadToEndAsync(cancellationToken);
        return mailBody;
    }
}