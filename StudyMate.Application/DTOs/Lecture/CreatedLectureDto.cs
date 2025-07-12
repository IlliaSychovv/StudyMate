namespace StudyMate.Application.DTOs.Lecture;

public record CreateLectureDto
{ 
    public string Topic { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    
    public int CourseId { get; set; }
}