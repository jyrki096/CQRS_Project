using Application.Auth.Services;
using Domain.Security;
using Domain.Security.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands;

public class RegisterUserHandler(UserManager<CustomIdentityUser> manager, IJwtSecurityService jwtSecurity, IMapper mapper)
    : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await manager.Users.AnyAsync(u => u.UserName == request.Dto.UserName))
        {
            throw new UserExistException($"Пользователь с UserName: {request.Dto.UserName} уже существует");
        }

        if (await manager.Users.AnyAsync(u => u.Email == request.Dto.Email))
        {
            throw new EmailExistException($"Пользователь с Email: {request.Dto.Email} уже существует");
        }

        var user = mapper.Map<CustomIdentityUser>(request.Dto);

        var result = await manager.CreateAsync(user, request.Dto.Password);

        if (!result.Succeeded)
        {
            throw new UserNotCreateException("Произошла ошибка во время создания пользователя");
        }

        var response = new IdentityUserResponseDto(user.UserName!, user.Email!, jwtSecurity.CreateToken(user));

        return new RegisterUserResult(response);
    }
}
