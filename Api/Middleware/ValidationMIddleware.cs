using Domain.Security.Dtos;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Api.Middleware;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method.Equals("GET")
            && context.Request.Path.Value!.ToLower().Contains("/register"))
        {
            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Пользователь ввёл неверный тип метода",
                Status = StatusCodes.Status400BadRequest,
                Detail = "Detail",
                Instance = context.Request.Path
            });
            return;
        }

        if (context.Request.Method.Equals("POST"))
        {
            context.Request.EnableBuffering();

            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

            context.Request.Body.Position = 0;

            try
            {
                var model = JsonSerializer.Deserialize<RegisterUserRequestDto>(body);

                if (model?.Password is not null && !IsValidPassword(model.Password))
                {
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsJsonAsync(new
                    {
                        Title = "Некорректный пароль",
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Detail",
                        Instance = context.Request.Path
                    });

                    return;
                }

                await next(context);
            }
            catch
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Ошибка валидации данных"
                });

               
            }
        }
        else
        {
            await next(context);
        }
    }

    private bool IsValidPassword(string password)
    {
        return password.Length >= 8 && password.Any(char.IsDigit);
    }
}
