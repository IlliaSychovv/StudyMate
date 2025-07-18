using Microsoft.EntityFrameworkCore;
using StudyMate.Domain.Entities;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Infrastructure.Data;

namespace StudyMate.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }
    
    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<Course> AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Course> UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return false;
        
        _context.Courses.Remove(course);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<Course>> GetCoursesByUserIdAsync(string userId)
    {
        return await _context.Courses
            .Where(x => x.Enrollments.Any(y => y.UserId == userId))
            .ToListAsync();
    }
    
    public async Task<List<Course>> GetCoursesByInstructorIdAsync(string instructorId)
    {
        return await _context.Courses
            .Where(x => x.InstructorId == instructorId)
            .ToListAsync();
    }
}