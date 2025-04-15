namespace Application.Exceptions;

internal class UserExistException : AuthorizationException
{
    public UserExistException(string message) : base(message)
    {
    }
}
