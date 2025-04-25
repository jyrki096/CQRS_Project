namespace Application.Security.Query;

public class LoginUserHandler(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurity)
    : IQueryHandler<LoginUserQuery, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var user = await manager.FindByEmailAsync(request.LoginRequest.Email);

        if (user is null)
        {
            throw new UserNotFoundException(request.LoginRequest.Email);
        }

        var result = await manager.CheckPasswordAsync(user, request.LoginRequest.Password);

        if (!result)
        {
            throw new InvalidLoginException("Неверная пара логин и пароль");
        }

        var token = jwtSecurity.CreateToken(user);
        var response = new IdentityUserResponseDto(user.UserName!, user.Email!, token);

        return new LoginUserResult(response);
    }
}
