using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Repositories;

public interface ITestRepository
{
    Task<List<Test>> GetTestsAsync();
    Task<Test> GetTestByIdAsync(int testId);
    Task<List<Test>> GetTestByCourseIdAsync(int courseId);
    Task<Test> AddTestAsync(Test test);
    Task<TestResult> AddTestResultAsync(TestResult testResult);
    Task<Test> UpdateTestAsync(Test test);
    Task<bool> DeleteTestAsync(int id);
    Task<List<TestResult>> GetByTestAndUserAsync(int testId, string userId);
}