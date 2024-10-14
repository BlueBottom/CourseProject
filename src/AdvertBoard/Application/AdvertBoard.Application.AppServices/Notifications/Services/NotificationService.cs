using System.Text.Json;
using AdvertBoard.Contracts.Contexts.Accounts.Events;
using AdvertBoard.Contracts.Contexts.Reviews.Events;
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

        /// <inheritdoc/>
        public async Task SendPasswordRecoveryCode(string email, string code, CancellationToken cancellationToken)
        {
            var message = new AskRecoveryPasswordCodeEvent(email, code);
            await _publishEndpoint.Publish(message, cancellationToken);
            _logger.LogInformation($"Отправлено событие {nameof(AskRecoveryPasswordCodeEvent)}: {{Message}}",
                JsonSerializer.Serialize(message));
        }

        /// <inheritdoc/>
        public async Task SendPasswordSuccessfullyRecovered(string email, CancellationToken cancellationToken)
        {
            var message = new PasswordRecoveredEvent(email);
            await _publishEndpoint.Publish(message, cancellationToken);
            _logger.LogInformation($"Отправлено событие {nameof(PasswordRecoveredEvent)}: {{Message}}",
                JsonSerializer.Serialize(message));
        }
    }
}