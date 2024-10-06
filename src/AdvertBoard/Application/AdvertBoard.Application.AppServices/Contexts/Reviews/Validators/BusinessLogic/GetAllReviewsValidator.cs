using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Validators;
using AdvertBoard.Contracts.Contexts.Reviews.Requests;
using FluentValidation;

namespace AdvertBoard.Application.AppServices.Contexts.Reviews.Validators.BusinessLogic;

/// <summary>
/// Валидатор, проверяющий правила бизнес логики при создании отзыва.
/// </summary>
public class GetAllReviewsValidator : BusinessLogicAbstractValidator<GetAllReviewsRequest>
{
    private readonly IUserRepository _userRepository;
    
    /// <summary>
    /// Инициализирует экземпляр <see cref="GetAllReviewsValidator"/>.
    /// </summary>
    public GetAllReviewsValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.UserId)
            .MustAsync(IsUserExists).WithMessage("Пользователь не был найден");
    }

    private async Task<bool> IsUserExists(Guid? id, CancellationToken cancellationToken)
    {
        return await _userRepository.IsExistsAsync(id!.Value, cancellationToken);
    }
}