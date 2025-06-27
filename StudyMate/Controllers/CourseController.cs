using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyMate.Application.DTOs;
using StudyMate.Application.Interfaces;

namespace StudyMate.Controllers;

[ApiController]
[Route("courses")]
public class CourseController : ControllerBase 
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        var courses = await _courseService.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourseByIdAsync(int id)
    {
        var course = await _courseService.GetByIdAsync(id);
        if (course == null)
            return NotFound();
        
        return Ok(course);
    }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CourseCreateDto dto)
    {
        // var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        // dto.InstructorId = userId;
        //
        // var result = await _courseService.CreateAsync(dto);
        // return Ok(result);
        
        var course = await _courseService.CreateAsync(dto);
        if (course == null)
            return BadRequest();
        
        return CreatedAtAction(nameof(GetCourseByIdAsync), new { course.Id }, course);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CourseUpdateDto dto)
    {
        var course = await _courseService.UpdateAsync(dto);
        return Ok(course);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var deleted = await _courseService.DeleteAsync(id);
        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}