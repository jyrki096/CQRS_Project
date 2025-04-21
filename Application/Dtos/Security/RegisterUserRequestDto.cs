namespace Application.Dtos.Security;

public record RegisterUserRequestDto(
    string FullName,
    string UserName,
    string Email,
    string Password
    );
