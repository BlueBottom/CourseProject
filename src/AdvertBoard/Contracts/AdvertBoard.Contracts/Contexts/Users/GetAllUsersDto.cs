using AdvertBoard.Contracts.Shared;

namespace AdvertBoard.Contracts.Contexts.Users;

public class GetAllUsersDto : PaginationRequest
{
    /// <summary>
    /// Строка поиска по имени и фамилии.
    /// </summary>
    public string? SearchNameString { get; set; }
    
    /// <summary>
    /// Минимальный рейтинг.
    /// </summary>
    public decimal? MinRating { get; set; }
    
    /// <summary>
    /// Максимальный рейтинг.
    /// </summary>
    public decimal? MaxRating { get; set; }
    
    /// <summary>
    /// Строка поиска по электронному почтовому адресу.
    /// </summary>
    public string? SearchEmailString { get; set; }
    
    /// <summary>
    /// Строка поиска по номеру телефона.
    /// </summary>
    public string? SearchPhoneString { get; set; }
    
    /// <summary>
    /// Стартовая дата регистрации пользователя.
    /// </summary>
    public DateTime? CreatedFromDate { get; set; }
    
    /// <summary>
    /// Конечная дата регистрации пользователя.
    /// </summary>
    public DateTime? CreateToDate { get; set; }
}