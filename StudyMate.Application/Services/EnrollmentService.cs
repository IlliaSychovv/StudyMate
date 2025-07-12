using StudyMate.Application.DTOs;
using StudyMate.Domain.Entities;
using Mapster;
using StudyMate.Application.DTOs.Course;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Application.Interfaces.Services;

namespace StudyMate.Application.Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollmentService(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<EnrollmentDto?> EnrollCourseAsync(int courseId, string studentId)
    {
        var course = await _enrollmentRepository.GetCourseByIdAsync(courseId);
        if (course == null)
            return null;
        
        var user = await _enrollmentRepository.GetUserByIdAsync(studentId);
        if (user == null)
            return null;
        
        var enrolled = await _enrollmentRepository.IsAlreadyEnrolledAsync(courseId, user.Id);
        if (enrolled)
            return null;

        var enrollment = new Enrollment
        {
            CourseId = courseId,
            UserId = user.Id,
            EnrolledAt = DateTime.Now
        };
        
        var addEnrollment = await _enrollmentRepository.AddEnrollmentAsync(enrollment);

        return addEnrollment.Adapt<EnrollmentDto>();
    }
    
    public async Task<List<CourseDto>> GetCoursesAsync(string studentId)
    {
        var course = await _enrollmentRepository.GetCoursesByStudentIdAsync(studentId);
        return course.Adapt<List<CourseDto>>();
    }

    public async Task<List<string?>> GetStudentsOfCourseAsync(int courseId, string requesterId)
    {
        var student = await _enrollmentRepository.GetStudentsOfCourseAsync(courseId);
        return student
            .Select(s => s.UserName)
            .ToList();
    }
}