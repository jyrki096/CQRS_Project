using Application.Auth.Query;
using Application.Auth.Services;
using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurity, IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto loginRequestDto)
    {
        return Results.Ok(await mediator.Send(new LoginUserQuery(loginRequestDto)));
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto dto)
    {
        if (await manager.Users.AllAsync(u => u.UserName == dto.Username))
        {
            return Results.BadRequest("Username уже занят");
        }

        if (await manager.Users.AllAsync(u => u.Email == dto.Email))
        {
            return Results.BadRequest("Email уже занят");
        }

        var user = new CustomIdentityUser
        {
            FullName = dto.FullName,
            Email = dto.Email,
            UserName = dto.Username,
            About = string.Empty
        };

        var result = await manager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            var response = new IdentityUserResponseDto(user.UserName!, user.Email!, jwtSecurity.CreateToken(user));

            return Results.Ok(new { result = response });
        }

        return Results.BadRequest(result.Errors);
    }
}
