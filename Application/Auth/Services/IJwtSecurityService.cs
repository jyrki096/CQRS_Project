namespace Application.Auth.Services;

public interface IJwtSecurityService
{
    string CreateToken(CustomIdentityUser user);
}
