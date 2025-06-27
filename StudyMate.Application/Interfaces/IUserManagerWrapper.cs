using Microsoft.AspNetCore.Identity;
using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces;

public interface IUserManagerWrapper
{
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<User?> FindByNameAsync(string userName);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<User?> GetUserByIdAsync(string userId);
    Task<IList<string>> GetRolesAsync(User user);
}