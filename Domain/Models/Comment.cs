using Domain.Security;

namespace Domain.Models;

public class Comment : Entity<CommentId>
{
    public CustomIdentityUser Author { get; set; } = default!;
    public Topic CurrentTopic { get; set; } = default!;
    public DateTime CreateAt { get; set; } = default!;
    public string Text { get; set; } = default!;

    public static Comment Create(CommentId commentId, CustomIdentityUser author,
        Topic currentTopic, DateTime createAt, string text)
    {
        ArgumentNullException.ThrowIfNull(commentId);
        ArgumentNullException.ThrowIfNull(author);
        ArgumentNullException.ThrowIfNull(currentTopic);

        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        return new()
        {
            Id = commentId,
            Author = author,
            CurrentTopic = currentTopic,
            CreateAt = createAt,
            Text = text
        };
    }
}
