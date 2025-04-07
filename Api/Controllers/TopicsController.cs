using Application.Dtos;
using Application.Topics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController(ITopicService topicService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TopicResponseDto>>> GetTopics()
        {
            return Ok(await topicService.GetTopicsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicResponseDto>> GetTopic(Guid id)
        {
            return Ok(await topicService.GetTopicAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<TopicResponseDto>> CreateTopic(CreateTopicDto createTopicDto)
        {
            return Ok(await topicService.CreateTopicAsync(createTopicDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TopicResponseDto>> UpdateTopic(Guid id, [FromBody] UpdateTopicDto updateTopicDto)
        {
            return Ok(await topicService.UpdateTopicAsync(id, updateTopicDto));
        }
    }
}
