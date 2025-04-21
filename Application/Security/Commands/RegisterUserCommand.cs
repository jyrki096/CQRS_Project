using Application.Dtos.Security;

namespace Application.Security.Commands;

public record RegisterUserCommand(RegisterUserRequestDto Dto) : ICommand<RegisterUserResult>;

public record RegisterUserResult(IdentityUserResponseDto Result);
