namespace StudyMate.Application.DTOs;

public record CourseUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; } 
    public int Price { get; set; }
}