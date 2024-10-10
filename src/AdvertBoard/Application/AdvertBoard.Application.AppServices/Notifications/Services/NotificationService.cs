using System.Text.Json;
using AdvertBoard.Contracts.Contexts.Reviews.Events;
using AdvertBoard.Contracts.Contexts.Users.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AdvertBoard.Application.AppServices.Notifications.Services
{
    /// <inheritdoc cref="INotificationService"/>
    public class NotificationService : INotificationService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<NotificationService> _logger;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NotificationService"/>.
        /// </summary>
        public NotificationService(IPublishEndpoint publishEndpoint, ILogger<NotificationService> logger)
        {
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task SendReviewStatusUpdated(Guid reviewId, Guid receiverUserId,
            CancellationToken cancellationToken)
        {
            var message = new ReviewStatusUpdatedEvent(reviewId, receiverUserId);
            await _publishEndpoint.Publish(message, cancellationToken);
            _logger.LogInformation($"Отправлено событие {nameof(ReviewStatusUpdatedEvent)}: {{Message}}",
                JsonSerializer.Serialize(message));
        }

        /// <inheritdoc/>
        public async Task SendUserRegistered(string userName, string userEmail, CancellationToken cancellationToken)
        {
            var message = new UserRegisteredEvent(userName, userEmail);
            await _publishEndpoint.Publish(message, cancellationToken);
            _logger.LogInformation($"Отправлено событие {nameof(UserRegisteredEvent)}: {{Message}}",
                JsonSerializer.Serialize(message));
        }
    }
}