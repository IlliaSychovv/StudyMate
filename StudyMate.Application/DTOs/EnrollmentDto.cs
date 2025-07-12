namespace StudyMate.Application.DTOs;

public record EnrollmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string UserId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public CourseDto? Course { get; set; }
}