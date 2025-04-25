namespace Application.Topics.Commands.CreateTopic;

public record CreateTopicCommand(CreateTopicDto createTopicDto) : ICommand<CreateTopicResult>;

public record CreateTopicResult(TopicResponseDto result);
