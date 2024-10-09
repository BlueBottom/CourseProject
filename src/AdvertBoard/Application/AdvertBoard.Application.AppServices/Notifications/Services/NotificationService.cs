using AdvertBoard.Contracts.Contexts.Reviews.Events;
using MassTransit;

namespace AdvertBoard.Application.AppServices.Notifications.Services
{
    /// <inheritdoc cref="INotificationService"/>
    public class NotificationService : INotificationService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NotificationService"/>.
        /// </summary>
        public NotificationService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        /// <inheritdoc/>
        public async Task SendReviewCreated(Guid reviewId, Guid receiverUserId, CancellationToken cancellationToken)
        {
            var message = new ReviewStatusUpdatedEvent(reviewId, receiverUserId);
            await _publishEndpoint.Publish(message, cancellationToken);
        }   
    }
}