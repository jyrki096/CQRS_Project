namespace Application.Dtos.Topic;

public record class CreateTopicDto(
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime EventStart
    );

