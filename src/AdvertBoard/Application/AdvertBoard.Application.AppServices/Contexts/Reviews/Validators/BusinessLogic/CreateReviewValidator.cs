using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Helpers;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при создании отзыва.
/// </summary>
public class CreateReviewValidator : BusinessLogicAbstractValidator<CreateReviewRequest>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="CreateReviewValidator"/>.
    /// </summary>
    public CreateReviewValidator(IReviewRepository reviewRepository, IHttpContextAccessor httpContextAccessor)
    {
        _reviewRepository = reviewRepository;
        _httpContextAccessor = httpContextAccessor;

        RuleFor(x => x.ReceiverUserId)
            .MustAsync(IsUserNotAlreadyLeftReview)
            .WithMessage("С этого аккаунта к уже был оставлен отзыв данному пользователю.")
            .Must(IsReviewNotReferenceToHimself)
            .WithMessage("Запрещено оставлять отзыв на самого себя.");
    }

    private bool IsReviewNotReferenceToHimself(Guid? receiverUserId)
    {
        var ownerUserId = _httpContextAccessor.GetAuthorizedUserId();
        return ownerUserId != receiverUserId!.Value;
    }

    private async Task<bool> IsUserNotAlreadyLeftReview(Guid? receiverUserId,
        CancellationToken cancellationToken)
    {
        var ownerUserId = _httpContextAccessor.GetAuthorizedUserId();
        return !await _reviewRepository.IsUserAlreadyLeftReview(ownerUserId, receiverUserId!.Value, cancellationToken);
    }
}