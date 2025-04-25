using Application.Topics.Queries.GetTopic;

namespace Application.Topics.Commands.JoinLeaveTopic;

public class JoinLeaveTopicHandler(IApplicationDbContext dbContext,
    IUserAccessor userAccessor)
    : ICommandHandler<JoinLeaveTopicCommand, JoinLeaveTopicResult>
{
    public async Task<JoinLeaveTopicResult> Handle(JoinLeaveTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = await GetTopicAsync(request.Id, cancellationToken);
        var currentUser = await GetCurrentUserAsync(cancellationToken);
        var organizer = topic.Users.FirstOrDefault(u => u.Role == ParticipantRole.Organizer)?.CurrentUser;

        if (organizer is not null && organizer.UserName == currentUser.UserName)
        {
            return await ToogleTopicStatusAsync(topic, cancellationToken);
        }

        return await UpdateCurrentUserStatusAsync(topic, currentUser, cancellationToken);
    }

    private async Task<CustomIdentityUser> GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var username = userAccessor.GetUsername();
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == username);

        if (user is null)
        {
            throw new UserNotFoundException(username, true);
        }

        return user;
    }

    private async Task<Topic> GetTopicAsync(Guid id, CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext.Topics
                                    .Include(t => t.Users)
                                    .ThenInclude(r => r.CurrentUser)
                                    .FirstOrDefaultAsync(t => t.Id == topicId, cancellationToken);

        if (result is null || result.IsDeleted)
        {
            throw new TopicNotFoundException(id);
        }

        return result;
    }

    private async Task<JoinLeaveTopicResult> ToogleTopicStatusAsync(Topic topic, CancellationToken cancellationToken)
    {
        var oldStatus = topic.IsVoided;
        topic.IsVoided = !oldStatus;

        dbContext.Topics.Update(topic);
        var isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        return new JoinLeaveTopicResult($"Статус изменился: {oldStatus} -> {topic.IsVoided}", isSuccess);
    }

    private async Task<JoinLeaveTopicResult> UpdateCurrentUserStatusAsync(Topic topic, CustomIdentityUser currentUser, CancellationToken cancellationToken)
    {
        var joinUser = topic.Users.FirstOrDefault(u => u.CurrentUser.UserName == currentUser.UserName);
        var detail = String.Empty;

        if (joinUser is null)
        {
            var relationship = Relationship.Create(
                id: RelationshipId.Of(Guid.NewGuid()),
                userId: currentUser.Id,
                user: currentUser,
                role: ParticipantRole.Participant,
                topicId: topic.Id,
                topic: topic
                );

            topic.Users.Add(relationship);
            detail = $"Вы присоединились ({topic.Id.Value})";
        }
        else
        {
            topic.Users.Remove(joinUser);
            detail = $"Вы покинули ({topic.Id.Value})";
        }

        var isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        return new JoinLeaveTopicResult(detail, isSuccess);
    }
}
