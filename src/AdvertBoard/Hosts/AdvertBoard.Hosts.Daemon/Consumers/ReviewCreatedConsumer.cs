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
    private readonly ILogger<ReviewCreatedConsumer> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="ReviewCreatedConsumer"/>.
    /// </summary>
    public ReviewCreatedConsumer(IUserRatingService userRatingService, ILogger<ReviewCreatedConsumer> logger)
    {
        _userRatingService = userRatingService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ReviewStatusUpdatedEvent> context)
    {
        _logger.LogInformation("Получено событие на пересчет рейтинга пользователя");
        await _userRatingService.UpdateRatingAsync(context.Message.ReceiverUserId, context.CancellationToken);
    }
}