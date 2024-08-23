using AdvertBoard.Domain.Base;

namespace AdvertBoard.Domain.Entities;

/// <summary>
/// Пользователь.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия.
    /// </summary>
    public string Lastname { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string Phone { get; set; }
    
    /// <summary>
    /// Электронный почтовый адрес.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Объявления.
    /// </summary>
    public ICollection<Advert> Adverts { get; set; }
    
    /// <summary>
    /// Отзывы, полученные от других пользователей.
    /// </summary>
    public ICollection<Review> ReceivedReviews { get; set; }
    
    /// <summary>
    /// Отзывы, оставленные другим пользователям.
    /// </summary>
    public ICollection<Review> LeftReviews { get; set; }
    
    /// <summary>
    /// Комментарии.
    /// </summary>
    public ICollection<Comment> Comments { get; set; }
}