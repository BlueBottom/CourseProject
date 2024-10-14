using AdvertBoard.Application.AppServices.Contexts.Email.Services;
using AdvertBoard.Contracts.Contexts.Accounts.Events;
using MassTransit;

namespace AdvertBoard.Hosts.Daemon.Consumers;

/// <summary>
/// Consumer для отправки письма об успешном восстановления пароля.
/// </summary>
public class PasswordRecoveredConsumer : IConsumer<PasswordRecoveredEvent>
{
    private readonly ILogger<PasswordRecoveredConsumer> _logger;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="PasswordRecoveredConsumer"/>.
    /// </summary>
    public PasswordRecoveredConsumer(ILogger<PasswordRecoveredConsumer> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    /// <summary>
    /// Обрабатывает полученное из очереди событие.
    /// </summary>
    /// <param name="context">Контекст события.</param>
    public async Task Consume(ConsumeContext<PasswordRecoveredEvent> context)
    {
        _logger.LogInformation($"Получено событие {nameof(PasswordRecoveredEvent)}.");
        await _emailService.SendMailAboutPasswordRecovering(context.Message.Email, context.CancellationToken);
    }
}