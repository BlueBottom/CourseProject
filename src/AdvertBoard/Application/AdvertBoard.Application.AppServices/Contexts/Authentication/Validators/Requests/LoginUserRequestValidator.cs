using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Validators.Requests;

/// <summary>
/// Валидатор запроса на логин пользователя.
/// </summary>
public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="LoginUserRequestValidator"/>.
    /// </summary>
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailRule();

        RuleFor(x => x.Password)!
            .PasswordRule();
    }
}