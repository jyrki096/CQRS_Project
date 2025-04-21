namespace Domain.Exceptions;

public class EmailAlreadyExistException : AuthorizationException
{
    public string Email { get; }

    public EmailAlreadyExistException(string email) : base($"Пользователь с Email: {email} уже существует")
    {
        Email = email;
    }
}
