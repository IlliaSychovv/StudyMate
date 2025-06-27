using Microsoft.AspNetCore.Http.HttpResults;
using StudyMate.Application.DTOs;

namespace StudyMate.Application.Interfaces;

public interface ICourseService
{
    Task<List<CourseDto>> GetAllAsync();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<CourseDto> CreateAsync(CourseCreateDto course);
    Task<CourseDto> UpdateAsync(CourseUpdateDto course);
    Task<bool> DeleteAsync(int id);
}