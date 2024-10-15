using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при логине пользователя.
/// </summary>
public class LoginUserValidator : BusinessLogicAbstractValidator<LoginUserRequest>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="LoginUserValidator"/>.
    /// </summary>
    /// <param name="userRepository">Сервис для работы с пользователями.</param>
    public LoginUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => new { x.Email, x.Password })
            .MustAsync((args, token) => IsCredsValid(args.Email!, args.Password!, token))
            .WithMessage("Пользователь с таким email и паролем не найден.");
    }

    private async Task<bool> IsCredsValid(string email, string password, CancellationToken cancellationToken)
    {
        var loginResponse = await _userRepository.FindByEmail(email, cancellationToken);
        if (loginResponse is null) return false;
        return (CryptoHelper.GetBase64Hash(password) == loginResponse.Password);
    }
}