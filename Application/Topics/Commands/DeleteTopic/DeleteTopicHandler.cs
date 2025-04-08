namespace Application.Topics.Commands.DeleteTopic;

public class DeleteTopicHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
{
    public async Task<DeleteTopicResult> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(request.id);
        var topic = await dbContext.Topics.FindAsync([topicId]);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.id);
        }

        topic.IsDeleted = true;
        topic.DeletedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(CancellationToken.None);

        return new DeleteTopicResult(true);
    }
}
