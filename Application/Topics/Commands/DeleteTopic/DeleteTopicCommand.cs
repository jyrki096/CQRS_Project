namespace Application.Topics.Commands.DeleteTopic;

public record DeleteTopicCommand(Guid id) : ICommand<DeleteTopicResult>;

public record DeleteTopicResult(bool isSuccess);
