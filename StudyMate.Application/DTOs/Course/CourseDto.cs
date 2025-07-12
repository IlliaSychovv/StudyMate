namespace StudyMate.Application.DTOs.Course;

public record CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int ReleaseYear { get; set; }
}