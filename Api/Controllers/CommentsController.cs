using Application.Dtos.Comment;
using Application.Comments.Commands;
using Application.Comments.Queries;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IResult> GetComments(Guid id)
    {
        return Results.Ok(await mediator.Send(new GetCommentsQuery(id)));
    }

    [HttpPost("{id}")]
    public async Task<IResult> CreateComment(Guid id, [FromBody] CommentRequestDto dto)
    {
        var response = await mediator.Send(new CreateCommentCommand(id, dto.Text));

        return Results.Created($"/comments/{id}", response.Result);
    }
}
