namespace AdvertBoard.Application.AppServices.Exceptions;

/// <summary>
/// Ошибка "Сущность не была найдена".
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="EntityNotFoundException"/>.
    /// </summary>
    public EntityNotFoundException() : base("Сущность не была найдена")
    {
        
    }

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="EntityNotFoundException"/>.
    /// </summary>
    public EntityNotFoundException(string message) : base(message)
    {
        
    }
}