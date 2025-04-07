namespace Application.Dtos;

public record class CreateTopicDto(
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime EventStart
    );

