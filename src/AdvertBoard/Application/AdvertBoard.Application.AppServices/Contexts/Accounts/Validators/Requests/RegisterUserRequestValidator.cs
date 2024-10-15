using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.Requests;

/// <summary>
/// Валидатор запроса на регистрацию пользователя.
/// </summary>
public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RegisterUserRequestValidator"/>.
    /// </summary>
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NameRule();

        RuleFor(x => x.Lastname)
            .LastnameRule();
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MatchPhoneNumberRule();

        RuleFor(x => x.Email)
            .EmailRule();

        RuleFor(x => x.Password)
            .PasswordRule();
    }
}