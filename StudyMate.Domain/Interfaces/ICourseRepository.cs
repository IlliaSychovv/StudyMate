using StudyMate.Domain.Entities;

namespace StudyMate.Domain.Interfaces;

public interface ICourseRepository
{
    Task<List<Course>> GetAllAsync();
    Task<Course?> GetByIdAsync(int id);
    Task<Course> AddAsync(Course course);
    Task<Course> UpdateAsync(Course course);
    Task<bool> DeleteAsync(int id);
}