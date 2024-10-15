namespace AdvertBoard.Application.AppServices.Exceptions;

/// <summary>
/// Ошибка "Нет прав доступа."
/// </summary>
public class ForbiddenException : Exception
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ForbiddenException"/>.
    /// </summary>
    public ForbiddenException() : base("Нет права доступа.")
    {
        
    }
}