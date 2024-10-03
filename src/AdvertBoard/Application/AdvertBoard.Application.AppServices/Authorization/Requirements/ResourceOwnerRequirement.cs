using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Application.AppServices.Authorization.Requirements;

/// <summary>
/// Ограничитель доступа по ресурсу.
/// </summary>
public class ResourceOwnerRequirement : IAuthorizationRequirement
{
}