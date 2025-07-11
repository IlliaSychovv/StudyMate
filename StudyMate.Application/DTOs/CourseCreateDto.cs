using System.Text.Json.Serialization;

namespace StudyMate.Application.DTOs;

public record CourseCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int ReleaseYear { get; set; }
    
    [JsonIgnore]
    public string? InstructorId { get; set; }
}