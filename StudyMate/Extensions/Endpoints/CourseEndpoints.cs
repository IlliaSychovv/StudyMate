using StudyMate.Application.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StudyMate.Application.DTOs.Course;

namespace StudyMate.Extensions.Endpoints;

public static class CourseEndpoints
{
    public static void AddCourseEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/courses");
        
        group.MapGet("/", async (ICourseService service) =>
        {
            var course = await service.GetAllAsync();
            return Results.Ok(course);
        }).WithTags("Courses");

        group.MapGet("/{id}", async (ICourseService service, int id) =>
        {
            var course = await service.GetByIdAsync(id);
            if (course == null)
                return Results.NotFound();
            
            return Results.Ok(course);
        }).WithTags("Courses");

        group.MapPost("/", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Instructor,Teacher")] async (HttpContext context, ICourseService service, CourseCreateDto dto) =>
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();
            dto.InstructorId = userId;
            
            var course = await service.CreateAsync(dto);
            return Results.Json(course, statusCode: 201);
        }).WithTags("Courses");

        group.MapPut("/", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Instructor,Teacher")] async (ICourseService service, CourseUpdateDto dto) =>
        {
            var updatedCourse = await service.UpdateAsync(dto);
            return Results.Ok(updatedCourse);
        }).WithTags("Courses");

        group.MapDelete("/{id}",[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Instructor")] async (ICourseService service, int id) =>
        {
            var deleted = await service.DeleteAsync(id);
            if (!deleted)
                return Results.NotFound();
            
            return Results.NoContent();
        }).WithTags("Courses");
    }
}