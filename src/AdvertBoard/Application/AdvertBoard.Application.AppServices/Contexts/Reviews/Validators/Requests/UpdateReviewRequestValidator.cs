using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Validators.Requests;

/// <summary>
/// Валидатор модели запроса на обновление отзыва.
/// </summary>
public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="UpdateReviewRequestValidator"/>.
    /// </summary>
    public UpdateReviewRequestValidator()
    {
        RuleFor(x => x.Rating)
            .RatingRule();
    }
}