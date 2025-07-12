using StudyMate.Application.DTOs;

namespace StudyMate.Application.Interfaces.Services;

public interface IEnrollmentService
{
    Task<EnrollmentDto?> EnrollCourseAsync(int courseId, string studentId);
    Task<List<CourseDto>> GetCoursesAsync(string studentId);
    Task<List<string>> GetStudentsOfCourseAsync(int courseId, string requesterId);
}