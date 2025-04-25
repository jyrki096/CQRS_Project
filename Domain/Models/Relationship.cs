using Domain.Enums;
using System.Net.Http.Headers;

namespace Domain.Models;

public class Relationship : Entity<RelationshipId>
{
    public required TopicId TopicReference { get; set; }
    public required Topic CurrentTopic { get; set; }

    public required string UserReference { get; set; }
    public required CustomIdentityUser CurrentUser { get; set; }

    public ParticipantRole Role { get; set; }

    public static Relationship Create(RelationshipId id, string userId,
        CustomIdentityUser user, ParticipantRole role, TopicId topicId,
        Topic topic)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(topicId);
        ArgumentNullException.ThrowIfNull(topic);


        return new()
        {
            Id = id,
            TopicReference = topicId,
            CurrentTopic = topic,
            UserReference = userId,
            CurrentUser = user,
            Role = role
        };
    }
}
