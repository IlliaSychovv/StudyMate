using StudyMate.Domain.Entities;

namespace StudyMate.Application.Interfaces.Repositories;

public interface IEnrollmentRepository
{
    Task<bool> IsAlreadyEnrolledAsync(int courseId, string studentId);
    Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);
    Task<List<Course>> GetCoursesByStudentIdAsync(string studentId); 
    Task<List<User>> GetStudentsOfCourseAsync(int courseId);
    Task<Course?> GetCourseByIdAsync(int courseId);
    Task<User?> GetUserByIdAsync(string userId);
}