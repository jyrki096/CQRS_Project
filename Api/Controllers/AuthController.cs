using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserManager<CustomIdentityUser> manager) : ControllerBase
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
            var response = new IdentityUserResponseDto(user.UserName!, user.Email!, "jwt");

            return Results.Ok(response);
        }

        return Results.Unauthorized();
    }
}
