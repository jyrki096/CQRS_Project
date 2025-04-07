using Application.Dtos;

namespace Application.Topics;

public interface ITopicService
{
    Task<List<TopicResponseDto>> GetTopicsAsync();
    Task<TopicResponseDto> GetTopicAsync(Guid id);
    Task<TopicResponseDto> CreateTopicAsync(CreateTopicDto topicCreateDto);
    Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicDto topicUpdateDto);
    Task DeleteTopicAsync(Guid id);
}
