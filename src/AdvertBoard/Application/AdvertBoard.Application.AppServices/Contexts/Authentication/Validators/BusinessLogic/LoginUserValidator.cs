using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Services;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при логине пользователя.
/// </summary>
public class LoginUserValidator : BusinessLogicAbstractValidator<LoginUserRequest>
{
    private readonly IUserService _userService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="LoginUserValidator"/>.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public LoginUserValidator(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => new { x.Email, x.Password })
            .MustAsync((args, token) => IsCredsValid(args.Email!, args.Password!, token))
            .WithMessage("Пользователь с таким email и паролем не найден.");
    }

    private async Task<bool> IsCredsValid(string email, string password, CancellationToken cancellationToken)
    {
        var loginResponse = await _userService.FindByEmail(email, cancellationToken);
        if (loginResponse is null) return false;
        return (CryptoHelper.GetBase64Hash(password) == loginResponse.Password);
    }
}