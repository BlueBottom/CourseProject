using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;

namespace AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;

/// <inheritdoc/>
public class UserRatingService : IUserRatingService
{
    private readonly IUserRepository _userRepository;
    private readonly IReviewRepository _reviewRepository;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserRatingService"/>.
    /// </summary>
    public UserRatingService(IUserRepository userRepository, IReviewRepository reviewRepository)
    {
        _userRepository = userRepository;
        _reviewRepository = reviewRepository;
    }
    
    private async Task<decimal?> EvaluateUserRatingAsync(Guid id, CancellationToken cancellationToken)
    {
        var rates = await _reviewRepository.GetAllRatesByUser(id, cancellationToken);
        if (rates.Length == 0) return null;
        return (decimal)rates.Average();
    }

    /// <inheritdoc/>
    public async Task UpdateRatingAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = await EvaluateUserRatingAsync(id, cancellationToken);
        await _userRepository.UpdateRatingAsync(id, rating, cancellationToken);
    }
}