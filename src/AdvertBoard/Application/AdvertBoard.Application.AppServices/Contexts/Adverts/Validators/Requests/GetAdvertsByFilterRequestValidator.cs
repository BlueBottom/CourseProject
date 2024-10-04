using AdvertBoard.Contracts.Contexts.Adverts.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Adverts.Validators.Requests;

/// <summary>
/// Валидатор запроса на получение объявлений по фильтру.
/// </summary>
public class GetAdvertsByFilterRequestValidator : AbstractValidator<GetAdvertsByFilterRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="GetAdvertsByFilterRequestValidator"/>.
    /// </summary>
    public GetAdvertsByFilterRequestValidator()
    {
        RuleFor(x => x.MinPrice)
            .GreaterThanOrEqualTo(0)
            .LessThan(int.MaxValue);
        
        RuleFor(x => x.MaxPrice)
            .GreaterThanOrEqualTo(0)
            .LessThan(int.MaxValue);

        RuleFor(x => x.Location)
            .GreaterThan(0)
            .LessThanOrEqualTo(95);

        RuleFor(x => x.SearchString)
            .MaximumLength(40);

        RuleFor(x => new { x.MinPrice, x.MaxPrice })
            .Must(arg => arg.MinPrice <= arg.MaxPrice)
            .When(request => request.MinPrice.HasValue && request.MaxPrice.HasValue)
            .WithMessage("Некорректно задан ценовой диапазон.");
    }
}