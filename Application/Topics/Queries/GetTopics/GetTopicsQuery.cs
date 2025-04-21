using Application.Dtos.Topic;

namespace Application.Topics.Queries.GetTopics;

public record GetTopicsQuery : IQuery<GetTopicsResult>;

public record GetTopicsResult(List<TopicResponseDto> Topics);
