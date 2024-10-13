using AdvertBoard.Application.AppServices.Contexts.Reviews.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;
using Moq;
using Xunit;

namespace AdvertBoard.Tests.Application.AppServicesTests.Contexts.Users.Services.Rating;

/// <summary>
/// Тест для провекри пересчета рейтинга пользователя.
/// </summary>
public class UserRatingServiceTests
{
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly CancellationToken _token;
    private readonly IUserRatingService _userRatingService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserRatingServiceTests"/>.
    /// </summary>
    public UserRatingServiceTests()
    {
        _reviewRepositoryMock = new Mock<IReviewRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _token = new CancellationTokenSource().Token;
        _userRatingService = new UserRatingService(_userRepositoryMock.Object, _reviewRepositoryMock.Object);
    }

    /// <summary>
    /// Проверяет, правильно ли рассчитан рейтинг, переданный в метод UpdateRatingAsync.
    /// </summary>
    /// <param name="rates">Оценки, поставленные пользователю.</param>
    [Theory]
    [InlineData(new int[] { 4, 5, 3, 4 })]
    [InlineData(new int[] { 5, 5, 5, 5 })]
    [InlineData(new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { })]
    public async Task UpdateRatingAsync_EvaluatesExpectedRating(int[] rates)
    {
        // Arrange
        var userId = Guid.NewGuid();

        _reviewRepositoryMock.Setup(r => r.GetAllRatesByUser(userId, _token))
            .ReturnsAsync(rates);

        decimal? expectedRating = rates.Length > 0 ? (decimal?)rates.Average() : null;

        // Act
        await _userRatingService.UpdateRatingAsync(userId, _token);

        // Assert
        _userRepositoryMock.Verify(u => u.UpdateRatingAsync(userId, expectedRating, _token), Times.Once);
    }
}