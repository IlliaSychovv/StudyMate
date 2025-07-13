using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.DTOs.Test;
using StudyMate.Application.DTOs.TestResult;

namespace StudyMate.Extensions.Endpoints;

public static class TestEndpoints
{
    public static void AddTestEndpoint(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/tests");
        
        group.MapGet("all", async (ITestService service) =>
        {
            var test = await service.GetAllTestAsync();
            return Results.Ok(test);
        }).WithTags("Tests");

        group.MapGet("{id}", async (ITestService service, int id) =>
        {
            var test = await service.GetTestsByIdAsync(id);
            return Results.Ok(test);
        }).WithTags("Tests");

        group.MapGet("{id}/course", async (ITestService service, int id) =>
        {
            var test = await service.GetByCourseIdAsync(id);
            return Results.Ok(test);
        }).WithTags("Tests");

        group.MapPost("create", async (ITestService service, TestCreateDto dto, string instructorId) =>
        {
            var test = await service.CreateTestAsync(dto, instructorId);
            if (test == null)
                return Results.BadRequest();
            
            return Results.Created($"/test/{test.Id}", test);
        }).WithTags("Tests");

        group.MapPost("submited", async (ITestService service, TestAnswerDto dto, string userId) =>
        {
            var test = await service.SubmitTestAsync(dto, userId);
            if (test == null)
                return Results.BadRequest();
            
            return Results.Ok(test);
        }).WithTags("Tests");

        group.MapGet("result", async (ITestService service, int testId, string userId) =>
        {
            var test = await service.GetResultsAsync(testId, userId);
            return Results.Ok(test);
        }).WithTags("Tests");
    }
}