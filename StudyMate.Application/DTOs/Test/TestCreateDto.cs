namespace StudyMate.Application.DTOs.Test;

public record TestCreateDto
{ 
    public string Title { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
}