namespace AdvertBoard.Application.AppServices.Exceptions;

/// <summary>
/// Базовый класс человеко-читаемых ошибок.
/// </summary>
public abstract class HumanReadableException : Exception
{
    /// <summary>
    /// Человеко-читаемое сообщение.
    /// </summary>
    public string HumanReadableMessage { get; }

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="HumanReadableException"/>.
    /// </summary>
    public HumanReadableException(string humanReadableMessage)
    {
        HumanReadableMessage = humanReadableMessage;
    }

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="HumanReadableException"/>.
    /// </summary>
    public HumanReadableException(string message, string humanReadableMessage) : base(message)
    {
        HumanReadableMessage = humanReadableMessage;
    }
}