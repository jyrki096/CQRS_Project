using Application.Exceptions;
using Application.Security.Services;

namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext, IMapper mapper, IUserAccessor userAccessor)
    : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(request.id);

        var topic = await dbContext.Topics.FindAsync([topicId]);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.id);
        }

        var username = userAccessor.GetUsername();
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == username);

        if (user is null)
        {
            throw new UserNotFoundException(username, true);
        }

        var organizerUsername = topic.Users
                                     .FirstOrDefault(u => u.Role == ParticipantRole.Organizer)
                                     ?.CurrentUser.UserName!;

        if (organizerUsername != username)
        {
            throw new UserNotOrganizerException(username, topic.Id.Value);
        }

        mapper.Map(request.updateTopicDto, topic);

        await dbContext.SaveChangesAsync(CancellationToken.None);

        return new UpdateTopicResult(topic.ToTopicResponseDto());
    }
}
