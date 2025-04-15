
namespace Application.Exceptions;

public class UserNotFoundException : AuthorizationException
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}
