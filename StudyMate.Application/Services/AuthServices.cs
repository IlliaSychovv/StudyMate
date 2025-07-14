using StudyMate.Application.Interfaces;
using StudyMate.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using StudyMate.Application.DTOs.Auth;
using StudyMate.Application.Interfaces.Services;

namespace StudyMate.Application.Services;

public class AuthServices : IAuthService
{
    private readonly IUserManagerWrapper _userManagerWrapper;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRoleService _roleService;

    public AuthServices(IUserManagerWrapper userManagerWrapper, IJwtTokenService jwtTokenService, RoleService roleService)
    {
        _userManagerWrapper = userManagerWrapper;
        _jwtTokenService = jwtTokenService;
        _roleService = roleService;
    }

    public async Task<IdentityResult> RegisterUserAsync(RegisterDto dto)
    {
        var user = dto.Adapt<User>();
        user.UserName = dto.Email;
        user.Role = dto.Role;
        
        var result = await _userManagerWrapper.CreateUserAsync(user, dto.Password);

        if (result.Succeeded)
        {
            var role = _roleService.GetRoleNameById(user.Role);
            await _roleService.CreateRoleAsync(role);
            await _roleService.AddUserToRoleAsync(user.Id, role);
        }
        
        return result;
    }

    public async Task<ResponseLoginDto?> LoginAsync(string username, string password)
    {
        var user = await _userManagerWrapper.FindByNameAsync(username);
        if (user == null)
            return null;
        
        var validPassword = await _userManagerWrapper.CheckPasswordAsync(user, password);
        if (!validPassword)
            return null;
        
        var roles = await _userManagerWrapper.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateTokenAsync(user, roles);

        var dto = user.Adapt<UserInfoDto>();
        dto.Roles = roles.ToList();

        return new ResponseLoginDto
        {
            Token = token,
            User = dto
        };
    }
}