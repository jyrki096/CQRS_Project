namespace Domain.Exceptions;

public class CommentException : Exception
{
    public CommentException(string message) : base(message)
    {
        
    }

    public CommentException(Guid id, string text)
        : base($"Проблема с комментарием: {text}. TopicId: ({id})")
    {
        
    }
}
