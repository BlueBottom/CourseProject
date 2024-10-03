namespace AdvertBoard.Contracts.Contexts.Users;

public class UpdateUserDto
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
}