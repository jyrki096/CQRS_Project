

namespace Application.Exceptions;

public class UserNotCreateException : AuthorizationException
{
    public UserNotCreateException(string message) : base(message)
    {
    }
}
