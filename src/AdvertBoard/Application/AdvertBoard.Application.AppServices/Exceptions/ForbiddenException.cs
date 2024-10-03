namespace AdvertBoard.Application.AppServices.Exceptions;

/// <summary>
/// Ошибка "Нет прав доступа."
/// </summary>
public class ForbiddenException : Exception
{
    public ForbiddenException() : base("Нет права доступа.")
    {
        
    }
}