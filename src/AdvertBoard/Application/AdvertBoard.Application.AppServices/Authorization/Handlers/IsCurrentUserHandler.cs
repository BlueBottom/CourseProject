﻿using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Contracts.Contexts.Users;
using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Application.AppServices.Authorization.Handlers;

/// <summary>
/// Обрабочик ограничителя доступа по владению ресурса <see cref="UserDto"/>.
/// </summary>
public class IsCurrentUserHandler : AuthorizationHandler<ResourceOwnerRequirement, UserDto>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        ResourceOwnerRequirement ownerRequirement,
        UserDto resource)
    {
        var claimId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (claimId is null) return Task.CompletedTask;
        var userId = Guid.Parse(claimId.Value);

        if (userId == resource.Id) context.Succeed(ownerRequirement);

        return Task.CompletedTask;
    }
}