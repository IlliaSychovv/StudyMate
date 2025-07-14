using StudyMate.Application.DTOs;
using Mapster;
using StudyMate.Application.DTOs.Course;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Application.Interfaces.Services;
using StudyMate.Domain.Entities;

namespace StudyMate.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<CourseDto>> GetAllAsync()
    {
        var course = await _courseRepository.GetAllAsync();
        return course.Adapt<List<CourseDto>>();
    }

    public async Task<CourseDto?> GetByIdAsync(int id)
    {
        var course = await _courseRepository.GetByIdAsync(id);
        if (course == null)
            return null;
        
        return course.Adapt<CourseDto>();
    }

    public async Task<CourseDto> CreateAsync(CourseCreateDto dto)
    { 
        var courseEntity = dto.Adapt<Course>();
        
        var course = await _courseRepository.AddAsync(courseEntity);
        return course.Adapt<CourseDto>();
    }
    
    
    public async Task<CourseDto> UpdateAsync(CourseUpdateDto dto)
    { 
        var existingCourse = await _courseRepository.GetByIdAsync(dto.Id);
        if (existingCourse == null)
            return null;

        var updatedCourse = await _courseRepository.UpdateAsync(existingCourse);
        return updatedCourse.Adapt<CourseDto>();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _courseRepository.DeleteAsync(id);
    }

    public async Task<List<CourseDto>> GetCoursesByUserIdAsync(string userId)
    {
        var courses = await _courseRepository.GetCoursesByUserIdAsync(userId);
        return courses.Adapt<List<CourseDto>>();
    }

    public async Task<List<CourseDto>> GetCoursesByInstructorIdAsync(string instructorId)
    {
        var courses = await _courseRepository.GetCoursesByInstructorIdAsync(instructorId);
        return courses.Adapt<List<CourseDto>>();
    }
}