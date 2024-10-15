using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.Requests;

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