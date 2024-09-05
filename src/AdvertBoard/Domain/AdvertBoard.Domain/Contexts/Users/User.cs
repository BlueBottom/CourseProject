using AdvertBoard.Domain.Base;
using AdvertBoard.Domain.Contexts.Adverts;
using AdvertBoard.Domain.Contexts.Comments;
using AdvertBoard.Domain.Contexts.Reviews;

namespace AdvertBoard.Domain.Contexts.Users;

/// <summary>
/// Пользователь.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? Lastname { get; set; }
    
    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// Электронный почтовый адрес.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// Объявления.
    /// </summary>
    public virtual ICollection<Advert>? Adverts { get; set; }
    
    /// <summary>
    /// Отзывы, полученные от других пользователей.
    /// </summary>
    public virtual ICollection<Review>? ReceivedReviews { get; set; }
    
    /// <summary>
    /// Отзывы, оставленные этим пользователем другим пользователям.
    /// </summary>
    public virtual ICollection<Review>? LeftReviews { get; set; }
    
    /// <summary>
    /// Комментарии.
    /// </summary>
    public virtual ICollection<Comment>? Comments { get; set; }
}