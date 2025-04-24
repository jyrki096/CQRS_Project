namespace Domain.Exceptions;

public class UserNotFoundException : AuthorizationException
{
    public UserNotFoundException(string email) 
        : base($"Пользователя с Email:{email} не существует")
    {

    }

    public UserNotFoundException(string username, bool isUsername)
       : base($"Пользователя с Email:{username} не существует")
    {

    }
}
