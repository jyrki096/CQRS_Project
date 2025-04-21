using Application.Dtos.Topic;

namespace Application.Topics.Commands.UpdateTopic;

public record UpdateTopicCommand(Guid id, UpdateTopicDto updateTopicDto) : ICommand<UpdateTopicResult>;

public record UpdateTopicResult(TopicResponseDto result);
