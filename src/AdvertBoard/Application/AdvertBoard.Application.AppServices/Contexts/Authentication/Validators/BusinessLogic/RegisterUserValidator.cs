using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Authentication.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проеряющий правила бизне логики для регистрации пользователя.
/// </summary>
public class RegisterUserValidator : BusinessLogicAbstractValidator<RegisterUserRequest>
{
    private readonly IUserService _userService;
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RegisterUserValidator"/>.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public RegisterUserValidator(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => x.Email)
            .MustAsync(IsEmailNotAlreadyTaken)
            .WithMessage("Пользователь с таким email уже существует.");

        RuleFor(x => x.Phone)
            .MustAsync(IsPhoneNotAlreadyTaken)
            .WithMessage("Пользователь с таким номером телефона уже существует.");
    }

    private async Task<bool> IsEmailNotAlreadyTaken(string email, CancellationToken cancellationToken)
    {
        return (await _userService.FindByEmail(email, cancellationToken)) is null;
    }

    private async Task<bool> IsPhoneNotAlreadyTaken(string phone, CancellationToken cancellationToken)
    {
        var phoneNormalized = PhoneHelper.NormalizePhoneNumber(phone);
        return !await _userService.IsExistByPhone(phoneNormalized, cancellationToken);
    }
}