namespace StudyMate.Application.Options;

public class JwtOptions
{
    public string Issuer { get; set; } = default;
    public string Audience { get; set; } = default;
    public string SecretKey { get; set; } = default;
    public int ExpirationInMinutes { get; set; }
}