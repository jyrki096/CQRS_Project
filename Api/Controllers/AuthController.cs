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
}
