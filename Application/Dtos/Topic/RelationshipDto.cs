namespace Application.Dtos.Topic;
public record RelationshipDto(
    RelationshipId Id,
    TopicId TopicReference,
    string UserReference,
    ParticipantRole Role,
    TopicResponseDto TopicDto,
    UserProfileDto UserDto
    );