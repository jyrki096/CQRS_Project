using Application.Dtos.Comment;

namespace Application.Comments.Commands;

public class CreateCommentHandler(IApplicationDbContext dbContext, IUserAccessor userAccessor, IMapper mapper)
    : ICommandHandler<CreateCommentCommand, CreateCommentResult>
{
    public async Task<CreateCommentResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var topicId = TopicId.Of(request.TopicId);
        var topic = await dbContext.Topics.FindAsync([topicId], cancellationToken);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.TopicId);
        }

        var username = userAccessor.GetUsername();
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user is null)
        {
            throw new UserNotFoundException(username, true);
        }

        var comment = Comment.Create(
            id: CommentId.Of(Guid.NewGuid()),
            text: request.Body,
            topic: topic,
            author: user
            );

        topic.Comments.Add(comment);

        var isSuccess = await dbContext.SaveChangesAsync(cancellationToken) > 0;

        if (isSuccess)
        {
            var result = mapper.Map<CommentDto>(comment);
            return new CreateCommentResult(result);
        }

        throw new CreateCommentException(topic.Id.Value, request.Body);
    }
}
