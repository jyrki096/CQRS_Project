namespace Application.Topics.Queries.GetTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext, IUserAccessor userAccessor, IMapper mapper)
    : ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userAccessor.GetUsername());
        var newTopic = mapper.Map<Topic>(request.createTopicDto);

        var relationship = Relationship.Create(
            id: RelationshipId.Of(Guid.NewGuid()),
            userId: user!.Id,
            user: user,
            role: ParticipantRole.Organizer,
            topicId: newTopic.Id,
            topic: newTopic
            );

        
        newTopic.Users.Add(relationship);

        await dbContext.Topics.AddAsync(newTopic);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        return new CreateTopicResult(newTopic.ToTopicResponseDto());
    }
}
