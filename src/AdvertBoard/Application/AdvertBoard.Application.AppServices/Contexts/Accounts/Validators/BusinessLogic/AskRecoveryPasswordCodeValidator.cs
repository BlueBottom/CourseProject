using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики для запроса на восстановление пароля.
/// </summary>
public class AskRecoveryPasswordCodeValidator : BusinessLogicAbstractValidator<AskRecoveryPasswordCodeRequest>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="AskRecoveryPasswordCodeValidator"/>.
    /// </summary>
    public AskRecoveryPasswordCodeValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(x => x.Email)
            .MustAsync(IsEmailExists)
            .WithMessage("Такой пользователь не зарегестрирован.");
    }
    
    private async Task<bool> IsEmailExists(string? email, CancellationToken cancellationToken)
    {
        return (await _userRepository.FindByEmail(email!, cancellationToken)) is not null;
    }
}