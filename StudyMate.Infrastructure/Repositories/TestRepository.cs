using Microsoft.EntityFrameworkCore;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Domain.Entities;
using StudyMate.Infrastructure.Data;

namespace StudyMate.Infrastructure.Repositories;

public class TestRepository : ITestRepository
{
    private readonly AppDbContext _context;

    public TestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Test>> GetTestsAsync()
    {
        return await _context.Tests.ToListAsync();
    }
    
    public async Task<Test?> GetTestByIdAsync(int testId)
    {
        return await _context.Tests
            .Include(t => t.Questions)
            .FirstOrDefaultAsync(t => t.Id == testId);
    }

    public async Task<List<Test>> GetTestByCourseIdAsync(int courseId)
    {
        return await _context.Tests
            .Where(t => t.CourseId == courseId)
            .ToListAsync();
    }

    public async Task<Test> AddTestAsync(Test test)
    {
        await _context.Tests.AddAsync(test);
        await _context.SaveChangesAsync();
        return test;
    }

    public async Task<TestResult> AddTestResultAsync(TestResult testResult)
    {
        await _context.TestResults.AddAsync(testResult);
        await _context.SaveChangesAsync();
        return testResult;
    }

    public async Task<Test> UpdateTestAsync(Test test)
    {
        _context.Tests.Update(test);
        await _context.SaveChangesAsync();
        return test;
    }

    public async Task<bool> DeleteTestAsync(int id)
    {
        var testToDelete = await _context.Tests.FindAsync(id);
        if (testToDelete == null)
            return false;
        
        _context.Tests.Remove(testToDelete);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TestResult>> GetByTestAndUserAsync(int testId, string userId)
    {
        return await _context.TestResults
            .Where(t => t.TestId == testId && t.UserId == userId)
            .ToListAsync();
    }
}