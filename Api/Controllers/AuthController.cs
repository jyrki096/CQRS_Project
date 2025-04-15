using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto loginRequestDto)
    {
        return Results.Ok(await mediator.Send(new LoginUserQuery(loginRequestDto)));
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto dto)
    {
        var response = await mediator.Send(new RegisterUserCommand(dto));
        return Results.Created("/auth/register", response.Result);
    }
}
