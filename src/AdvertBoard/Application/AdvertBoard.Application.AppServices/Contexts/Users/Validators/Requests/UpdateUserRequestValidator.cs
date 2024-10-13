using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Validators.Requests;

/// <summary>
/// Валидатор модели запроса на обновление.
/// </summary>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UpdateUserRequestValidator"/>.
    /// </summary>
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailRule();

        RuleFor(x => x.Phone)
            .NotEmpty()
            .MatchPhoneNumberRule();

        RuleFor(x => x.Name)
            .NameRule();

        RuleFor(x => x.Lastname)
            .LastnameRule();
    }
}