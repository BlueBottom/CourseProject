using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using FluentValidation;
using AdvertBoard.Application.AppServices.Helpers;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Validators.Requests;

/// <summary>
/// Валидатор запроса на создание объявления.
/// </summary>
public class CreateAdvertRequestValidator : AbstractValidator<CreateAdvertRequest>
{
    
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateAdvertRequestValidator"/>.
    /// </summary>
    public CreateAdvertRequestValidator()
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

        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.Phone)
            .MatchPhoneNumberRule();

        RuleFor(x => x.Address)
            .MaximumLength(250);
    }
}