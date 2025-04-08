using Microsoft.EntityFrameworkCore;

namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext)
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

        MapModels(topic, request.updateTopicDto);

        await dbContext.SaveChangesAsync(CancellationToken.None);

        return new UpdateTopicResult(topic.ToTopicResponseDto());
    }

    private void MapModels(Topic topic, UpdateTopicDto updateTopicDto)
    {
        topic.Title = updateTopicDto.Title ?? topic.Title;
        topic.Summary = updateTopicDto.Summary ?? topic.Summary;
        topic.TopicType = updateTopicDto.TopicType ?? topic.TopicType;
        topic.EventStart = updateTopicDto.EventStart;
        topic.Location = Location.Of(
            updateTopicDto.Location.City,
            updateTopicDto.Location.Street
            );
    }
}
