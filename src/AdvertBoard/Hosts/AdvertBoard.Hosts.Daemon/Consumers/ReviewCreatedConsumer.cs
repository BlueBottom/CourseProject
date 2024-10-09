using AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;
using AdvertBoard.Contracts.Contexts.Reviews.Events;
using MassTransit;

namespace AdvertBoard.Hosts.Daemon.Consumers;

/// <summary>
/// Consumer для пересчета рейтинга.
/// </summary>
public class ReviewCreatedConsumer : IConsumer<ReviewStatusUpdatedEvent>
{
    private readonly IUserRatingService _userRatingService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ReviewCreatedConsumer"/>.
    /// </summary>
    public ReviewCreatedConsumer(IUserRatingService userRatingService)
    {
        _userRatingService = userRatingService;
    }

    public async Task Consume(ConsumeContext<ReviewStatusUpdatedEvent> context)
    {
        await _userRatingService.EvaluateUserRatingAsync(context.Message.ReceiverUserId, CancellationToken.None);
        Console.WriteLine("Рейтинг пользователя посчитан.");
    }
}