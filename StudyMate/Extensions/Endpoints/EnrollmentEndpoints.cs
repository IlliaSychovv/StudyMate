using StudyMate.Application.Interfaces.Services;

namespace StudyMate.Extensions.Endpoints;

public static class EnrollmentEndpoints
{
    public static void AddEnrollmentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/enrollments");
        
        group.MapPost("/", async (IEnrollmentService service, int courseId, string studentId) =>
        {
            var enroll = await service.EnrollCourseAsync(courseId, studentId);
            if (enroll == null)
                return Results.NotFound();
            
            return Results.Ok(enroll);
        }).WithTags("Enrollments");

        group.MapGet("/my", async (IEnrollmentService service, string studentId) =>
        {
            var courses = await service.GetCoursesAsync(studentId);
            return Results.Ok(courses);
        }).WithTags("Enrollments");

        group.MapGet("/students", async (IEnrollmentService service, int courseId, string requestedId) =>
        {
            var students = await service.GetStudentsOfCourseAsync(courseId, requestedId);
            return Results.Ok(students);
        }).WithTags("Enrollments");
    }
}