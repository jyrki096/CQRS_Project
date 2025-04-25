using Application.Data.DataBaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Security.Claims;
using Domain.Enums;

namespace Infrastructure.Security.Auth;

public class TopicDeletionRequirement : IAuthorizationRequirement
{

}


public class TopicDeletionRequirementHandler(IApplicationDbContext dbContext,
    IHttpContextAccessor httpContextAccessor)
    : AuthorizationHandler<TopicDeletionRequirement>
{
    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, TopicDeletionRequirement requirement)
    {
        var usedId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (usedId is null)
        {
            context.Fail();
        }

        var routeValue = httpContextAccessor.HttpContext?.Request.RouteValues;
        var value = routeValue?.FirstOrDefault(x => x.Key == "id").Value?.ToString();

        if (String.IsNullOrEmpty(value))
        {
            context.Fail();
        }

        var topicId = TopicId.Of(Guid.Parse(value));
        var relationship = await dbContext.Relationships
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync(r => r.UserReference == usedId.ToString()
                                                && r.TopicReference == topicId);

        if (relationship?.Role == ParticipantRole.Organizer)
        {
            context.Succeed(requirement);
        }
    }
}
