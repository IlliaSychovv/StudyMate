namespace StudyMate.Application.DTOs.Question;

public record QuestionCreateDto
{ 
    public string Text { get; set; }
    public string Options { get; set; }
    public int CorrectAnswer { get; set; }
    public int TestId { get; set; }
}