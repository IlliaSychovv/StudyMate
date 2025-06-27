namespace StudyMate.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string InstructorId { get; set; }
    public int Price { get; set; }
    public int ReleaseYear { get; set; }
    public User Instructor { get; set; }
    
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}