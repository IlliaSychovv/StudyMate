using Mapster;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.DTOs.Test;
using StudyMate.Domain.Entities;
using StudyMate.Application.DTOs.TestResult;

namespace StudyMate.Application.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    private readonly ICourseRepository _courseRepository;

    public TestService(ITestRepository testRepository, ICourseRepository courseRepository)
    {
        _testRepository = testRepository;
        _courseRepository = courseRepository;
    }

    public async Task<List<TestDto>> GetAllTestAsync()
    {
        var test = await _testRepository.GetTestsAsync();
        return test.Adapt<List<TestDto>>();
    }

    public async Task<TestDto?> GetTestsByIdAsync(int id)
    {
        var test = await _testRepository.GetTestByIdAsync(id);
        return test.Adapt<TestDto>();
    }

    public async Task<List<TestDto>> GetByCourseIdAsync(int courseId)
    {
        var test = await _testRepository.GetTestByCourseIdAsync(courseId);
        return test.Adapt<List<TestDto>>();
    }

    public async Task<TestDto> CreateTestAsync(TestCreateDto dto, string instructorId)
    {
        var course = await _courseRepository.GetByIdAsync(dto.CourseId);
        if (course == null || course.InstructorId != instructorId)
            throw new ArgumentException("Invalid course");

        var test = dto.Adapt<Test>();
        var addedTest = await _testRepository.AddTestAsync(test);
        return addedTest.Adapt<TestDto>();
    }

    public async Task<TestResultDto> SubmitTestAsync(TestAnswerDto dto, string userId)
    {
        var test = await _testRepository.GetTestByIdAsync(dto.TestId);
        if (test == null)
            throw new ArgumentException("Test not found");
        
        var questions = test.Questions.ToList();
        if (questions.Count == 0)
            throw new ArgumentException("Test has no questions");

        if (dto.Answers.Count != questions.Count)
            throw new ArgumentException("Number of answers doesn't match number of questions");

        int score = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            if (dto.Answers[i] == questions[i].CorrectAnswer)
            {
                score++;
            }
        }

        var testResult = new TestResult
        {
            UserId = userId,
            TestId = dto.TestId,
            Score = score,
            TotalQuestions = questions.Count,
            AnsweredAt = DateTime.UtcNow
        };
        
        var savedResult = await _testRepository.AddTestAsync(testResult);
        return savedResult.Adapt<TestResultDto>();
    }

    public async Task<List<TestResult>> GetResultsAsync(int testId, string userId)
    {
        var result = await _testRepository.GetByTestAndUserAsync(testId, userId);
        return result.Adapt<List<TestResult>>();
    }
}