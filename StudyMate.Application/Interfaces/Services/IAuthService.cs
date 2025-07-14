using Microsoft.AspNetCore.Identity;
using StudyMate.Application.DTOs;
using StudyMate.Application.DTOs.Auth;

namespace StudyMate.Application.Interfaces.Services;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(RegisterDto dto);
    Task<ResponseLoginDto?> LoginAsync(string username, string password);
}