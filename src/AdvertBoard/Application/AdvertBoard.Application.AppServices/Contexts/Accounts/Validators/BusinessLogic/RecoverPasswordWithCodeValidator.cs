using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;

/// <summary>
/// Валидатор для проверки бизнес логики изменения пароля посредством его восстановления.
/// </summary>
public class RecoverPasswordWithCodeValidator : BusinessLogicAbstractValidator<RecoverPasswordWithCodeRequest>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RecoverPasswordWithCodeValidator"/>.
    /// </summary>
    public RecoverPasswordWithCodeValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Email)
            .MustAsync(IsEmailExists);
    }
    
    private async Task<bool> IsEmailExists(string? email, CancellationToken cancellationToken)
    {
        return (await _userRepository.FindByEmail(email!, cancellationToken)) is not null;
    }
}