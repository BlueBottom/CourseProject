namespace AdvertBoard.Application.AppServices.Contexts.Users.Services.Password;

/// <summary>
/// Сервис с логикой смены пароля пользователя.
/// </summary>
public interface IUserPasswordService
{
    /// <summary>
    /// Метод-запрос на восстановление пароля пользователя.
    /// </summary>
    /// <param name="email">"Электронный адрес почты.</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task RecoverPassword(string email, CancellationToken cancellationToken);
}