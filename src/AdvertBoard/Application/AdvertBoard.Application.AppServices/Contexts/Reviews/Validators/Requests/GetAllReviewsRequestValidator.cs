using AdvertBoard.Contracts.Common;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Validators.Requests;

/// <summary>
/// Валидатор модели запроса на получение отзывов.
/// </summary>
public class GetAllReviewsRequestValidator : AbstractValidator<GetAllReviewsRequest>
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetAllReviewsRequestValidator"/>.
    /// </summary>
    public GetAllReviewsRequestValidator(IValidator<PaginationRequest> paginationValidator)
    {
        Include(paginationValidator);
        
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}