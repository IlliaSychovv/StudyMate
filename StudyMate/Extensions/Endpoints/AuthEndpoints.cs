using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.DTOs;

namespace StudyMate.Extensions.Endpoints;

public static class AuthEndpoints
{
    public static void AddAuthEndpoints(this WebApplication app)
    {
        var auth = app.MapGroup("api/v1/auth");

        auth.MapPost("/register", async (IAuthService service, RegisterDto dto) =>
        {
            var register = await service.RegisterUserAsync(dto);
            if (register.Succeeded)
                return Results.Ok("User registered successfully");

            return Results.BadRequest(register);
        }).WithTags("Auth");
        
        auth.MapPost("/login", async (IAuthService service, LoginDto dto) =>
        {
            var login = await service.LoginAsync(dto.Email, dto.Password);
            if (login == null)
                return Results.Unauthorized();
            
            return Results.Ok(new { Token = login });
        }).WithTags("Auth");
    }
}