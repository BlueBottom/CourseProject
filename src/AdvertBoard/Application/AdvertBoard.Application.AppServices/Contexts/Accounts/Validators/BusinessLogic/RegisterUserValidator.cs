using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Accounts.Requests;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Accounts.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проеряющий правила бизне логики для регистрации пользователя.
/// </summary>
public class RegisterUserValidator : BusinessLogicAbstractValidator<RegisterUserRequest>
{
    private readonly IUserRepository _userRepository;
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RegisterUserValidator"/>.
    /// </summary>
    /// <param name="userRepository">Сервис для работы с пользователями.</param>
    public RegisterUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Email)
            .MustAsync(IsEmailNotAlreadyTaken)
            .WithMessage("Пользователь с таким email уже существует.");

        RuleFor(x => x.Phone)
            .MustAsync(IsPhoneNotAlreadyTaken)
            .WithMessage("Пользователь с таким номером телефона уже существует.");
    }

    private async Task<bool> IsEmailNotAlreadyTaken(string? email, CancellationToken cancellationToken)
    {
        return (await _userRepository.FindByEmail(email!, cancellationToken)) is null;
    }

    private async Task<bool> IsPhoneNotAlreadyTaken(string? phone, CancellationToken cancellationToken)
    {
        var phoneNormalized = PhoneHelper.NormalizePhoneNumber(phone!);
        return !await _userRepository.IsExistByPhone(phoneNormalized, cancellationToken);
    }
}