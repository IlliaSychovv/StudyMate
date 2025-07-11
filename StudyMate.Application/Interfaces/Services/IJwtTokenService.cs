using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Services;

public interface IJwtTokenService
{
    string GenerateTokenAsync(User user, IList<string> roles);
}