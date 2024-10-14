using AdvertBoard.Application.AppServices.Contexts.Email.Services;
using AdvertBoard.Contracts.Contexts.Accounts.Events;
using MassTransit;

namespace AdvertBoard.Hosts.Daemon.Consumers;

/// <summary>
/// Consumer для отправки кода для восстановления пароля.
/// </summary>
public class RecoveryPasswordCodeSentConsumer : IConsumer<AskRecoveryPasswordCodeEvent>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<RecoveryPasswordCodeSentConsumer> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="RecoveryPasswordCodeSentConsumer"/>.
    /// </summary>
    public RecoveryPasswordCodeSentConsumer(IEmailService emailService, ILogger<RecoveryPasswordCodeSentConsumer> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    /// <summary>
    /// Обрабатывает полученне из очереди событие.
    /// </summary>
    /// <param name="context">Контекст события.</param>
    public async Task Consume(ConsumeContext<AskRecoveryPasswordCodeEvent> context)
    {
        _logger.LogInformation($"Получено событие {nameof(AskRecoveryPasswordCodeEvent)}.");
        await _emailService.SendRecoveryPasswordCode(context.Message.Email, context.Message.Code, context.CancellationToken);
    }
}