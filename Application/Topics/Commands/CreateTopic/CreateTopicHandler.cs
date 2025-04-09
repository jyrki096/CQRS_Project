namespace Application.Topics.Queries.GetTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext, IMapper mapper)
    : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var newTopic = mapper.Map<Topic>(request.createTopicDto);

        await dbContext.Topics.AddAsync(newTopic);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        return new CreateTopicResult(newTopic.ToTopicResponseDto());
    }

    private Topic CreateTopic(CreateTopicDto topic)
    {
        return Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            topic.Title,
            topic.EventStart,
            topic.Summary,
            topic.TopicType,
            Location.Of(topic.Location.City, topic.Location.Street)
            );
    }
}
