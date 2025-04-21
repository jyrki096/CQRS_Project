namespace Domain.Exceptions;

public class UserNotFoundException : AuthorizationException
{
    public string Email { get; }

    public UserNotFoundException(string email) : base($"Пользователя с Email:{email} не существует")
    {
        Email = email;
    }
}
