namespace StudyMate.Domain.Entities;

public class Enrollment
{
    public int Id { get; set; }
    
    public int CourseId { get; set; }
    public Course Course { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public DateTime EnrolledAt { get; set; }
}