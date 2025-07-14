using StudyMate.Domain.Entities;

namespace StudyMate.Application.DTOs.Auth;

public record ResponseLoginDto
{
    public string Token { get; set; } = string.Empty;
    public UserInfoDto User { get; set; } = new ();
}

public record UserInfoDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public UserRole Role { get; set; } 
    public List<string> Roles { get; set; } = new ();
}