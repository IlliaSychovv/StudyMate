using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Services;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string roleName);
    Task<bool> AddUserToRoleAsync(string userId, string roleName);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<bool> IsUserInRoleAsync(string userId, string roleName);
    string GetRoleNameById(UserRole role);
}