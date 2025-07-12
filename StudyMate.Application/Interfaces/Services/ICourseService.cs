using StudyMate.Application.DTOs;
using StudyMate.Application.DTOs.Course;

namespace StudyMate.Application.Interfaces.Services;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<CourseDto> CreateAsync(CourseCreateDto course);
    Task<CourseDto> UpdateAsync(CourseUpdateDto course);
    Task<bool> DeleteAsync(int id);
}