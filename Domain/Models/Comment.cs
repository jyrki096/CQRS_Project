using Domain.Security;

namespace Domain.Models;

public class Comment : Entity<CommentId>
{
    public CustomIdentityUser Author { get; set; } = default!;
    public Topic CurrentTopic { get; set; } = default!;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public string Text { get; set; } = default!;

    public static Comment Create(CommentId id, CustomIdentityUser author,
        Topic topic, string text)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(author);
        ArgumentNullException.ThrowIfNull(topic);

        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        return new()
        {
            Id = id,
            Author = author,
            CurrentTopic = topic,
            Text = text
        };
    }
}
