using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Repositories;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<Course> AddAsync(Course course);
    Task<Course> UpdateAsync(Course course);
    Task<bool> DeleteAsync(int id);
    Task<List<Course>> GetCoursesByUserIdAsync(string userId);
    Task<List<Course>> GetCoursesByInstructorIdAsync(string instructorId);
}