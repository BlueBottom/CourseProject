using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Validators.Requests;

/// <summary>
/// Валидатор запроса на объявление объявления.
/// </summary>
public class UpdateAdvertRequestValidator : AbstractValidator<UpdateAdvertRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UpdateAdvertRequestValidator"/>.
    /// </summary>
    public UpdateAdvertRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(80);

        RuleFor(x => x.Description)
            .MaximumLength(1000);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .LessThan(int.MaxValue);

        RuleFor(x => x.Location)
            .NotEmpty()
            .GreaterThan(0)
            .LessThanOrEqualTo(95);

        RuleFor(x => x.Phone)
            .MatchPhoneNumberRule();

        RuleFor(x => x.Address)
            .MaximumLength(250);
    }
}