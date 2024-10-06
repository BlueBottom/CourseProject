namespace AdvertBoard.Contracts.Common;

/// <summary>
/// Модель ошибки валидации.
/// </summary>
public class ValidationApiError : ApiError
{
    /// <summary>
    /// Ошибки.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; set; }
}