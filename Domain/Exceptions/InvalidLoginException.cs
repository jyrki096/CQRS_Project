namespace Domain.Exceptions;

public class InvalidLoginException : AuthorizationException
{
    public InvalidLoginException(string message) : base(message)
    {
    }
}
