namespace Application.Auth.Commands;

public record RegisterUserCommand(RegisterUserRequestDto Dto) : ICommand<RegisterUserResult>;

public record RegisterUserResult(IdentityUserResponseDto Result);
