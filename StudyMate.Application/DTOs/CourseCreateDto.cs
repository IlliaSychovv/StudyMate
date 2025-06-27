namespace StudyMate.Application.DTOs;

public class CourseCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int ReleaseYear { get; set; }
    public string InstructorId { get; set; }
}