using Application.Comments.Commands;
using Application.Comments.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(IMediator mediator) : ControllerBase
    {

        [HttpGet("TopicId")]
        public async Task<IResult> GetComments(Guid topicId)
        {
            return Results.Ok(await mediator.Send(new GetCommentsQuery(topicId)));
        }

        [HttpPost("TopicId")]
        public async Task<IResult> CreateComment(Guid topicId, string text)
        {
            var response = await mediator.Send(new CreateCommentCommand(topicId, text));

            return Results.Created($"/comments/{topicId}", response.Result);
        }
    }
}
