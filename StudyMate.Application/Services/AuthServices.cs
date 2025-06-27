using StudyMate.Application.Interfaces;
using StudyMate.Application.DTOs;
using StudyMate.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace StudyMate.Application.Services;

public class AuthServices : IAuthService
{
    private readonly IUserManagerWrapper _userManagerWrapper;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthServices(IUserManagerWrapper userManagerWrapper, IJwtTokenService jwtTokenService)
    {
        _userManagerWrapper = userManagerWrapper;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterDto dto)
    {
        var user = dto.Adapt<User>();
        user.UserName = dto.Email;
        
        return await _userManagerWrapper.CreateUserAsync(user, dto.Password);
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userManagerWrapper.FindByNameAsync(username);
        if (user == null)
            return null;
        
        var validPassword = await _userManagerWrapper.CheckPasswordAsync(user, password);
        if (!validPassword)
            return null;
        
        var roles = await _userManagerWrapper.GetRolesAsync(user);
        
        return _jwtTokenService.GenerateTokenAsync(user, roles);
    }
}