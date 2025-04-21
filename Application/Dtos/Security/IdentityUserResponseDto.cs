namespace Application.Dtos.Security;

public record IdentityUserResponseDto(
    string Username,
    string Email,
    string JwtToken
    );

