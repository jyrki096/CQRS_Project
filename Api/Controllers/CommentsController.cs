using Application.Dtos.Comment;
using Application.Comments.Commands;
using Application.Comments.Queries;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IResult> GetComments(Guid topicId)
    {
        return Results.Ok(await mediator.Send(new GetCommentsQuery(topicId)));
    }

    [HttpPost("{id}")]
    public async Task<IResult> CreateComment(Guid topicId, CommentRequestDto dto)
    {
        var response = await mediator.Send(new CreateCommentCommand(topicId, dto.Text));

        return Results.Created($"/comments/{topicId}", response.Result);
    }
}
