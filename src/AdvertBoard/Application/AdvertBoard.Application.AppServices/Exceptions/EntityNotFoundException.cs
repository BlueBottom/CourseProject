namespace AdvertBoard.Application.AppServices.Exceptions;

/// <summary>
/// Ошибка "Сущность не была найдена".
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base("Сущность не была найдена")
    {
        
    }
    
    public EntityNotFoundException(string message) : base(message)
    {
        
    }
}