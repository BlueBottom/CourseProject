using AdvertBoard.Contracts.Enums;
using AdvertBoard.Domain.Base;

namespace AdvertBoard.Domain.Entities;

/// <summary>
/// Объявление. 
/// </summary>
public class Advert : BaseEntity
{
    /// <summary>
    /// Наименование.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Цена.
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public AdvertStatus Status { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь, выложивший объявление.
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Категория, в которой находится объявление.
    /// </summary>
    public Category Category { get; set; }
    
    /// <summary>
    /// Изображения объявления.
    /// </summary>
    public ICollection<Image> Images { get; set; }
    
    /// <summary>
    /// Комментарии к объявлению.
    /// </summary>
    public ICollection<Comment> Comments { get; set; }
}