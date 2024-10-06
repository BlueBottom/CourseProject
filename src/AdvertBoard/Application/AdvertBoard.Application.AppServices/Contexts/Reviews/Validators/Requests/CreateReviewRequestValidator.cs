using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Validators.Requests;

/// <summary>
/// Валидатор модели запроса на создание отзыва.
/// </summary>
public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateReviewRequestValidator" />.
    /// </summary>
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(400);

        RuleFor(x => x.ReceiverUserId)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .RatingRule();
    }
}