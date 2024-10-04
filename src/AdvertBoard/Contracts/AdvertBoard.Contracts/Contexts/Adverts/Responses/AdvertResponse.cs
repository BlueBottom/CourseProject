using AdvertBoard.Contracts.Contexts.Users;
using AdvertBoard.Contracts.Enums;

namespace AdvertBoard.Contracts.Contexts.Adverts.Responses;

/// <summary>
/// Модель объявления.
/// </summary>
public class AdvertResponse
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public AdvertStatus Status { get; set; }
    
    /// <summary>
    /// Регион.
    /// </summary>
    public int Location { get; set; }
    
    /// <summary>
    /// Точный адрес объявления.
    /// </summary>
    public string? Address { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь, выложивший объявление.
    /// </summary>
    public virtual ShortUserDto? User { get; set; }
    
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public ICollection<Guid>? ImageIds { get; set; }
    
    /// <summary>
    /// Комментарии к объявлению.
    /// </summary>
    public ICollection<Guid>? CommentIds { get; set; }
}