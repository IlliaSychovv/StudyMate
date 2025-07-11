using Microsoft.EntityFrameworkCore;
using StudyMate.Domain.Entities;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Infrastructure.Data;

namespace StudyMate.Infrastructure.Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;

    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsAlreadyEnrolledAsync(int courseId, string studentId)
    {
        return await (_context.Enrollments.AnyAsync(e => e.CourseId == courseId && e.UserId == studentId));
    }

    public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<List<Course>> GetCoursesByStudentIdAsync(string studentId)
    {
        return await _context.Enrollments
            .Where(e => e.UserId == studentId)
            .Select(e => e.Course)
            .ToListAsync();
    }

    public async Task<List<User>> GetStudentsOfCourseAsync(int courseId)
    {
        return await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.User)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int courseId)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _context.Users.FindAsync(userId);
    }
}