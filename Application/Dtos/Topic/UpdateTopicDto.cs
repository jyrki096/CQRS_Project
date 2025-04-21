namespace Application.Dtos.Topic;

public record class UpdateTopicDto(
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime EventStart
    );

