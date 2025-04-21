namespace Application.Dtos.Security;

public record LoginRequestDto(
    string Email,
    string Password
    );
