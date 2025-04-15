
namespace Application.Exceptions;

public class EmailExistException : AuthorizationException
{
    public EmailExistException(string message) : base(message)
    {
    }
}
