using Application.Dtos.Comment;

namespace Application.Comments.Queries;

public record GetCommentsQuery(Guid TopicId) : IQuery<GetCommentsResult>;

public record GetCommentsResult(IEnumerable<CommentDto> Result);

