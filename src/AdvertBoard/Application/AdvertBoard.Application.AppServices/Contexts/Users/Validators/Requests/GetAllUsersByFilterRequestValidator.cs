using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Users.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Validators.Requests;

/// <summary>
/// Валидатор модели запроса для получения коллекции пошльзователей по фильтру.
/// </summary>
public class GetAllUsersByFilterRequestValidator : AbstractValidator<GetAllUsersByFilterRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="GetAllUsersByFilterRequestValidator"/>.
    /// </summary>
    public GetAllUsersByFilterRequestValidator(IValidator<PaginationRequest> paginationValidator)
    {
        Include(paginationValidator);
        RuleFor(x => x.MinRating)
            .RatingRule();
        
        RuleFor(x => x.MaxRating)
            .RatingRule();
        
        RuleFor(x => new {x.MinRating, x.MaxRating})
            .Must(arg => arg.MinRating <= arg.MaxRating)
            .When(request => request.MinRating.HasValue && request.MaxRating.HasValue)
            .WithMessage("Некорректно задан диапазон рейтинга пользователя.");
        
        RuleFor(x => new {x.CreatedFromDate, x.CreateToDate})
            .Must(arg => arg.CreatedFromDate <= arg.CreateToDate)
            .When(request => request.MinRating.HasValue && request.MaxRating.HasValue)
            .WithMessage("Некорректно задан диапазон времени регистрации пользователя.");

        RuleFor(x => x.SearchNameString)
            .MaximumLength(50);

        RuleFor(x => x.SearchEmailString)
            .MaximumLength(50);

        RuleFor(x => x.SearchPhoneString)
            .MaximumLength(11);
    }
}