using Application.Dtos.Topic;
using Application.Topics.Commands.JoinLeaveTopic;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TopicsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IResult> GetTopics()
    {
        return Results.Ok(await mediator.Send(new GetTopicsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetTopic(Guid id)
    {
        return Results.Ok(await mediator.Send(new GetTopicQuery(id))); 
    }

    [HttpPost]
    public async Task<IResult> CreateTopic(CreateTopicDto createTopicDto)
    {
        var response = await mediator.Send(new CreateTopicCommand(createTopicDto));
        return Results.Created($"/topics/{response.result.Id}", response.result);
    }

    [HttpPut("{id}")]
    public async Task<IResult> UpdateTopic(Guid id, [FromBody] UpdateTopicDto updateTopicDto)
    {
        var response = await mediator.Send(new UpdateTopicCommand(id, updateTopicDto));
        return Results.Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteTopic(Guid id)
    {
        var isSuccess = await mediator.Send(new DeleteTopicCommand(id));

        return Results.Ok(isSuccess);
    }

    [HttpPost("{id}")]
    public async Task<IResult> JoinLeaveTopic(Guid id)
    {  
        return Results.Ok(await mediator.Send(new JoinLeaveTopicCommand(id)));
    }
}
