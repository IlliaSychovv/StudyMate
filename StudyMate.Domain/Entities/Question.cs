namespace StudyMate.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Options { get; set; }
    public int CorrectAnswer { get; set; }
    public int TestId { get; set; }
    public Test Test { get; set; }
}