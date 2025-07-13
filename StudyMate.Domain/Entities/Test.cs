namespace StudyMate.Domain.Entities;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CourseId { get; set; }
    
    public Course Course { get; set; }
    public ICollection<Question> Questions { get; set; } = new List<Question>(); 
    public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}