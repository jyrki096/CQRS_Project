using Api.Security.Services;
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
public class AuthController(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurity) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto loginRequestDto)
    {
        var user = await manager.FindByEmailAsync(loginRequestDto.Email);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        var result = await manager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (result)
        {
            var token = jwtSecurity.CreateToken(user);
            var response = new IdentityUserResponseDto(user.UserName!, user.Email!, token);

            return Results.Ok(response);
        }

        return Results.Unauthorized();
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
