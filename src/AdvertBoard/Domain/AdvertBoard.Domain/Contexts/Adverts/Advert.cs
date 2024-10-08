using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Base;
using AdvertBoard.Domain.Contexts.Categories;
using AdvertBoard.Domain.Contexts.Comments;
using AdvertBoard.Domain.Contexts.Images;
using AdvertBoard.Domain.Contexts.Users;

namespace AdvertBoard.Domain.Contexts.Adverts;

/// <summary>
/// Объявление. 
/// </summary>
public class Advert : BaseEntity
{
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
    public AdvertStatus StatusId { get; set; }
    
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
    public virtual User? User { get; set; }
    
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Категория, в которой находится объявление.
    /// </summary>
    public virtual Category? Category { get; set; }
    
    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public virtual ICollection<Image>? Images { get; set; }
    
    /// <summary>
    /// Комментарии к объявлению.
    /// </summary>
    public virtual ICollection<Comment>? Comments { get; set; }
}