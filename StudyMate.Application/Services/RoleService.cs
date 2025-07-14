using StudyMate.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using StudyMate.Domain.Entities;

namespace StudyMate.Application.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<bool> CreateRoleAsync(string roleName)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        return result.Succeeded;
    }

    public async Task<bool> AddUserToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;
        
        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }
    
    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new List<string>();
        
        var result = await _userManager.GetRolesAsync(user);
        return result.ToList();
    }

    public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;
        
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public string GetRoleNameById(UserRole role)
    {
        return role switch
        {
            UserRole.Instructor => "Instructor",
            UserRole.Teacher => "Teacher",
            UserRole.Student => "Student",
            _ => throw new ArgumentOutOfRangeException("Wrong role")
        };
    }
}