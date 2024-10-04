using AdvertBoard.Application.AppServices.Helpers;
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
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);

        RuleFor(x => x.Password)!
            .PasswordRule();
    }
}