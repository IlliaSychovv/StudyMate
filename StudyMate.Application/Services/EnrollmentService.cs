using StudyMate.Application.DTOs;
using StudyMate.Application.Interfaces;
using StudyMate.Domain.Entities;
using Mapster;

namespace StudyMate.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<string> EnrollCourseAsync(int courseId, string studentId)
    {
        var course = await _enrollmentRepository.GetCourseByIdAsync(courseId);
        if (course == null)
            return "Course not found";
        
        var user = await _enrollmentRepository.GetUserByIdAsync(studentId);
        if (user == null)
            return "User not found";
        
        var enrolled = await _enrollmentRepository.IsAlreadyEnrolledAsync(courseId, user.Id);
        if (enrolled)
            return "User already enrolled";

        var enrollment = new Enrollment
        {
            CourseId = courseId,
            UserId = user.Id
        };
        
        var addEnrollment = await _enrollmentRepository.AddEnrollmentAsync(enrollment);

        return $"Enrollment successful, enroll ID: {addEnrollment.Id}";
    }
    
    public async Task<List<CourseDto>> GetCoursesAsync(string studentId)
    {
        var course = await _enrollmentRepository.GetCoursesByStudentIdAsync(studentId);
        return course.Adapt<List<CourseDto>>();
    }

    public async Task<List<string>> GetStudentsOfCourseAsync(int courseId, string requesterId)
    {
        var student = await _enrollmentRepository.GetStudentsOfCourseAsync(courseId);
        return student
            .Select(s => s.UserName)
            .ToList();
    }
}