using Microsoft.AspNetCore.Mvc;
using StudyMate.Application.Interfaces;
using StudyMate.Domain.Entities;

namespace StudyMate.Controllers;

[ApiController]
[Route("api/v1/enrollments")]
public class EnrollmentController : ControllerBase
{
    private readonly IEnrollmentService _enrollmentService;

    public EnrollmentController(IEnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }

    [HttpPost("enroll")]
    public async Task<IActionResult> Enroll([FromQuery] int courseId, [FromQuery] string studentId)
    {
        var enroll = await _enrollmentService.EnrollCourseAsync(courseId, studentId);
        
        if (enroll == null)
            return NotFound("Enrollment not found");
        
        return Ok(enroll);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetCourses([FromQuery] string studentId)
    {
        var courses = await _enrollmentService.GetCoursesAsync(studentId);
        
        return Ok(courses);
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudentsOfCourse([FromQuery] int courseId, [FromQuery] string requesterId)
    {
        var students = await _enrollmentService.GetStudentsOfCourseAsync(courseId, requesterId);
        return Ok(students);
    }
}