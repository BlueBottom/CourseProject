using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.Requests;

/// <summary>
/// Валидатор запроса на изменеие пароля путем его восстановления.
/// </summary>
public class RecoverPasswordWithCodeRequestValidator : AbstractValidator<RecoverPasswordWithCodeRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RecoverPasswordWithCodeRequestValidator"/>.
    /// </summary>
    public RecoverPasswordWithCodeRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailRule();

        RuleFor(x => x.Code)
            .Matches(@"^\d{6}$");

        RuleFor(x => x.Password)
            .PasswordRule();
    }
}