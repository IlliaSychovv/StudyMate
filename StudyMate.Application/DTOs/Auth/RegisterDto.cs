using StudyMate.Domain.Entities;

namespace StudyMate.Application.DTOs.Auth;

public record RegisterDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Student;
}