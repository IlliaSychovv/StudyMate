using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateTokenAsync(User user, IList<string> roles);
}