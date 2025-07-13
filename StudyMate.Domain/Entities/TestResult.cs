namespace StudyMate.Domain.Entities;

public class TestResult
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int TestId { get; set; }
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public DateTime AnsweredAt { get; set; }
    
    public User User { get; set; }
    public Test Test { get; set; }
}