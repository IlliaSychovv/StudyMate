namespace StudyMate.Domain.Entities;

public class Lecture
{
    public int Id { get; set; }
    public string Topic { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    
    public int CourseId { get; set; }
    public Course Course { get; set; }
}