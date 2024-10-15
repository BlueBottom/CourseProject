using AdvertBoard.Application.AppServices.Contexts.Email.Services;
using AdvertBoard.Contracts.Contexts.Accounts.Events;
using MassTransit;

namespace AdvertBoard.Hosts.Daemon.Consumers.Contexts.Accounts;

/// <summary>
/// Consumer для отправки сообщения о регистрации на email. 
/// </summary>
public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<UserRegisteredConsumer> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserRegisteredConsumer"/>.
    /// </summary>
    public UserRegisteredConsumer(IEmailService emailService, ILogger<UserRegisteredConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    /// <summary>
    /// Обрабатывает полученне из очереди событие.
    /// </summary>
    /// <param name="context">Контекст события.</param>
    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        _logger.LogInformation($"Получено событие {nameof(UserRegisteredEvent)}.");
        await _emailService.SendMailAboutRegistration(context.Message.Name, context.Message.Email,
            context.CancellationToken);
    }
}