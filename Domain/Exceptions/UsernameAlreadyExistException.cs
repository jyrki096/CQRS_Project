namespace Domain.Exceptions;

public class UsernameAlreadyExistException : AuthorizationException
{
    public string UserName { get; }

    public UsernameAlreadyExistException(string userName) : base($"Пользователь с UserName: {userName} уже существует")
    {
        UserName = userName;
    }
}
