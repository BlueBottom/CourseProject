using AdvertBoard.Application.AppServices.Contexts.Users.Services.Rating;
using AdvertBoard.Contracts.Contexts.Reviews.Events;
using MassTransit;

namespace AdvertBoard.Hosts.Daemon.Consumers.Contexts.Reviews;

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

    /// <summary>
    /// Обрабатывает полученне из очереди событие.
    /// </summary>
    /// <param name="context">Контекст события.</param>
    public async Task Consume(ConsumeContext<ReviewStatusUpdatedEvent> context)
    {
        _logger.LogInformation($"Получено событие {nameof(ReviewStatusUpdatedEvent)}.");
        await _userRatingService.UpdateRatingAsync(context.Message.ReceiverUserId, context.CancellationToken);
    }
}