using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.Requests;

/// <summary>
/// Валидатор запроса на восстановление пароля.
/// </summary>
public class AskRecoveryPasswordCodeRequestValidator : AbstractValidator<AskRecoveryPasswordCodeRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="AskRecoveryPasswordCodeRequestValidator"/>.
    /// </summary>
    public AskRecoveryPasswordCodeRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailRule();
    }
}