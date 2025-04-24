namespace Application.Dtos.Topic;

public record class TopicResponseDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime? EventStart,
    bool IsVoided
    List<UserProfileDto> Users
    );


