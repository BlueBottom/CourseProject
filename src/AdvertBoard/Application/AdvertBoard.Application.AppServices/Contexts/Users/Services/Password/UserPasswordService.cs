// using AdvertBoard.Application.AppServices.Helpers;
// using AdvertBoard.Application.AppServices.Notifications.Services;
// using Microsoft.Extensions.Caching.Distributed;
//
// namespace AdvertBoard.Application.AppServices.Contexts.Users.Services.Password;
//
// /// <inheritdoc/>
// public class UserPasswordService : IUserPasswordService
// {
//     private const string RedisPasswordRecoveryPrefix = "user_recovery_code:";
//     private readonly INotificationService _notificationService;
//     private readonly IDistributedCache _distributedCache;
//
//     /// <summary>
//     /// Инициализирует экземпляр класса <see cref="UserPasswordService"/>.
//     /// </summary>
//     public UserPasswordService(INotificationService notificationService, IDistributedCache distributedCache)
//     {
//         _notificationService = notificationService;
//         _distributedCache = distributedCache;
//     }
//
//     /// <inheritdoc/>
//     public Task RecoverPassword(string email, CancellationToken cancellationToken)
//     {
//         _distributedCache.PutByKeyAsync($"{RedisPasswordRecoveryPrefix}{email}", );
//     }
// }