using StudyMate.Application.Interfaces;
using StudyMate.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace StudyMate.Infrastructure.Wrapper;

public class UserManagerWrapper : IUserManagerWrapper
{
    private readonly UserManager<User> _userManager;

    public UserManagerWrapper(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<User?> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }
    
    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}