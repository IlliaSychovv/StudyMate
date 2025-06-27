using Microsoft.AspNetCore.Identity;
using StudyMate.Application.DTOs;

namespace StudyMate.Application.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterUserAsync(RegisterDto dto);
    Task<string> LoginAsync(string username, string password);
}