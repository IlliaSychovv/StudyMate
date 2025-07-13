using StudyMate.Application.DTOs.Test;
using StudyMate.Application.DTOs.TestResult;
using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Services;

public interface ITestService
{
    Task<List<TestDto>> GetAllTestAsync();
    Task<TestDto?> GetTestsByIdAsync(int id);
    Task<List<TestDto>> GetByCourseIdAsync(int courseId);
    Task<TestDto> CreateTestAsync(TestCreateDto dto, string instructorId);
    Task<TestResultDto> SubmitTestAsync(TestAnswerDto dto, string userId);
    Task<List<TestResultDto>> GetResultsAsync(int testId, string userId);
}