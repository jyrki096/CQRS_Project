namespace Application.Auth.Query;

public record LoginUserQuery(LoginRequestDto LoginRequest) : IQuery<LoginUserResult>;

public record LoginUserResult(IdentityUserResponseDto Result);

