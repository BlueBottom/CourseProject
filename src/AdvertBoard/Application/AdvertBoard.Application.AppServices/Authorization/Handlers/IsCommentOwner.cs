using System.Security.Claims;
using AdvertBoard.Application.AppServices.Authorization.Requirements;
using AdvertBoard.Contracts.Contexts.Comments;
using Microsoft.AspNetCore.Authorization;

namespace AdvertBoard.Application.AppServices.Authorization.Handlers;

public class IsCommentOwner : AuthorizationHandler<ResourceOwnerRequirement, CommentDto>
{
    /// <inheritdoc/>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement ownerRequirement, 
        CommentDto resource)
    {
        var claimId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (claimId is null) return Task.CompletedTask;
        var userId = Guid.Parse(claimId.Value);

        if (userId == resource.UserId) context.Succeed(ownerRequirement);

        return Task.CompletedTask;
    }
}
