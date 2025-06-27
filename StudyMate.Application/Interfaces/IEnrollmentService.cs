using StudyMate.Application.DTOs;

namespace StudyMate.Application.Interfaces;

public interface IEnrollmentService
{
    Task<string> EnrollCourseAsync(int courseId, string studentId);
    Task<List<CourseDto>> GetCoursesAsync(string studentId);
    Task<List<string>> GetStudentsOfCourseAsync(int courseId, string requesterId);
}