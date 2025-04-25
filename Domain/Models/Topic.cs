namespace Domain.Models;

public class Topic : Entity<TopicId>
{
    public string Title { get; set; } = default!;
    public DateTime EventStart { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string TopicType { get; set; } = default!;
    public Location Location { get; set; } = default!;
    public bool IsVoided { get; set; } = default!;

    public List<Relationship> Users { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    public static Topic Create(
        TopicId topicId, string title, DateTime eventStart,
        string summary, string topicType, Location location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(summary);
        ArgumentException.ThrowIfNullOrWhiteSpace(topicType);

        return new()
        {
            Id = topicId,
            Title = title,
            EventStart = eventStart,
            TopicType = topicType,
            Location = location,
            Summary = summary
        };
    }

}
