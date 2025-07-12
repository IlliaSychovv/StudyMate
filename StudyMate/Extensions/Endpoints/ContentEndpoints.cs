using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.DTOs.Lecture;

namespace StudyMate.Extensions.Endpoints;

public static class ContentEndpoints
{
    public static void AddLectureEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/courses");

        group.MapGet("{courseId}/lectures", async (IContentService service, int couresId) =>
        {
            var course = await service.GetLecturesByCourseAsync(couresId);
            return Results.Ok(course);
        }).WithTags("Lectures");

        group.MapGet("lectures/{lectureId}", async (IContentService service, int lectureId) =>
        {
            var lecture = await service.GetLecturesByCourseAsync(lectureId);
            return Results.Ok(lecture);
        }).WithTags("Lectures");

        group.MapPost("{courseId}/lectures", async (IContentService service, int courseId, CreateLectureDto dto) =>
        {
            var lecture = await service.CreateLectureAsync(courseId, dto);
            return Results.Created($"{courseId}/lectures/{lecture}", lecture);
            //return Results.Ok(lecture);
        }).WithTags("Lectures");

        group.MapDelete("lectures/{id}", async (IContentService service, int id) =>
        {
            var deletedLecture = await service.DeleteLectureAsync(id);
            if (!deletedLecture)
                return Results.NotFound();
            
            return Results.NoContent();
        }).WithTags("Lectures");
    }
}