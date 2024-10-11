using AdvertBoard.Application.AppServices.Contexts.Email.Services;
using AdvertBoard.Contracts.Contexts.Users.Events;
using MassTransit;

namespace AdvertBoard.Hosts.Daemon.Consumers;

/// <summary>
/// Consumer для отправки сообщения о регистрации на email. 
/// </summary>
public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
{
    private readonly IUserEmailService _userEmailService;
    private readonly ILogger<UserRegisteredConsumer> _logger;

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserRegisteredConsumer"/>.
    /// </summary>
    public UserRegisteredConsumer(IUserEmailService userEmailService, ILogger<UserRegisteredConsumer> logger)
    {
        _userEmailService = userEmailService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        _logger.LogInformation("Получено событие на отправку письма о регистрации пользователя");
        await _userEmailService.SendEmailAboutRegistration(context.Message.Name, context.Message.Email,
            context.CancellationToken);
    }
}