using System.Reflection;

namespace AdvertBoard.Application.AppServices.Helpers;

/// <summary>
/// Класс для работы с встроенными ресурсами.
/// </summary>
public static class EmbeddedResourceHelper
{
    public static async Task<string> GetEmbeddedResourceAsString(string fullResourceName, CancellationToken cancellationToken)
    {
        var mailBodyStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(
            $"{Assembly.GetExecutingAssembly().GetName().Name}.{fullResourceName}")!;
        using var mailBodyStreamReader = new StreamReader(mailBodyStream);
        var mailBody = await mailBodyStreamReader.ReadToEndAsync(cancellationToken);
        return mailBody;
    }
}