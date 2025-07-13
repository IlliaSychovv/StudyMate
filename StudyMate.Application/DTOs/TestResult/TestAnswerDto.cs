using StudyMate.Application.DTOs.Question;

namespace StudyMate.Application.DTOs.TestResult;

public record TestAnswerDto
{
    public int TestId { get; set; }
    public List<int> Answers { get; set; }
}