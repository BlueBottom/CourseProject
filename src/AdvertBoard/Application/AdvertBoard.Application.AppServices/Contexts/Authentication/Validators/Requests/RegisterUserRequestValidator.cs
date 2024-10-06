using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Validators.Requests;

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
            .NotEmpty()
            .MaximumLength(25);

        RuleFor(x => x.Lastname)
            .MaximumLength(25);

        RuleFor(x => x.Phone)
            .MatchPhoneNumberRule();
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .PasswordRule();
    }
}