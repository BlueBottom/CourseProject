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

    /// <summary>
    /// Инициализирует экземпляр класса <see cref="UserRegisteredConsumer"/>.
    /// </summary>
    public UserRegisteredConsumer(IUserEmailService userEmailService)
    {
        _userEmailService = userEmailService;
    }

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        await _userEmailService.SendEmailAboutRegistration(context.Message.Name, context.Message.Email,
            context.CancellationToken);
    }
}