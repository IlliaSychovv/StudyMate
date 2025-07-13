namespace StudyMate.Application.DTOs.Question;

public record QuestionDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Options { get; set; }
    public int CorrectAnswer { get; set; }
    public int TestId { get; set; }
}