using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using StudyMate.Application.DTOs;
using StudyMate.Application.Interfaces;

namespace StudyMate.Controllers;

[ApiController]
[Route("controller")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IAuthService authService, IJwtTokenService jwtTokenService)
    {
        _authService = authService;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var register = await _authService.RegisterUserAsync(dto);
        
        if (register.Succeeded)
            return Ok("User registered successfully!");
        
        return BadRequest(register);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var login = await _authService.LoginAsync(dto.Email, dto.Password);
        
        if (login == null)
            return Unauthorized("Invalid username or password!");
        
        return Ok(new { Token = login });
    }
}