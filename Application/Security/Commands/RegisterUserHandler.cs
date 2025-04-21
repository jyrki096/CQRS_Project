namespace Application.Security.Commands;

public class RegisterUserHandler(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurity, IMapper mapper)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await manager.Users.AnyAsync(u => u.UserName == request.Dto.UserName))
        {
            throw new UsernameAlreadyExistException(request.Dto.UserName);
        }

        if (await manager.Users.AnyAsync(u => u.Email == request.Dto.Email))
        {
            throw new EmailAlreadyExistException(request.Dto.Email);
        }

        var user = mapper.Map<CustomIdentityUser>(request.Dto);

        var result = await manager.CreateAsync(user, request.Dto.Password);

        if (result.Succeeded)
        {
            var response = new IdentityUserResponseDto(user.UserName!, user.Email!, jwtSecurity.CreateToken(user));
            return new RegisterUserResult(response);
            
        }

        throw new AuthorizationException(string.Join(";", result.Errors));
    }
}
