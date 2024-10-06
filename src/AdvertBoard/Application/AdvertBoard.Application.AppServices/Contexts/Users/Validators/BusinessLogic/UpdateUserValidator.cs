using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при обновлении пользователя.
/// </summary>
public class UpdateUserValidator : BusinessLogicAbstractValidator<UpdateUserRequest>
{
    private readonly IUserRepository _userRepository;
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UpdateUserValidator"/>.
    /// </summary>
    public UpdateUserValidator(IUserRepository userRepository)
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