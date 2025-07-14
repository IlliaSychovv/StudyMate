using Microsoft.AspNetCore.Identity;
using StudyMate.Domain.Entities;
using StudyMate.Infrastructure.Data;
using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.Services;

namespace StudyMate.Infrastructure.SeedData;

public class DbSeeder
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IRoleService _roleService;

    public DbSeeder(AppDbContext context, UserManager<User> userManager, RoleService roleService)
    {
        _context = context;
        _userManager = userManager;
        _roleService = roleService;
    }

    public async Task Seed()
    {
        if (_context.Courses.Any() || _context.Enrollments.Any() || _context.Users.Any())
            return;
        
        await _roleService.CreateRoleAsync("Teacher");
        await _roleService.CreateRoleAsync("Instructor");
        await _roleService.CreateRoleAsync("Student");

        var instructor = new User
        {
            UserName = "instructor",
            Email = "instructor@gmail.com",
            Role = UserRole.Instructor
        };

        var student = new User
        {
            UserName = "student",
            Email = "student@gmail.com",
            Role = UserRole.Student
        };

        var instructorResult = await _userManager.CreateAsync(instructor, "Test@123");
        if (!instructorResult.Succeeded)
            throw new Exception("Error while creating instructor: " + string.Join(", ", instructorResult.Errors.Select(e => e.Description)));

        var studentResult = await _userManager.CreateAsync(student, "Test@123");
        if (!studentResult.Succeeded)
            throw new Exception("Error while creating student: " + string.Join(", ", studentResult.Errors.Select(e => e.Description)));

        instructor = await _userManager.FindByNameAsync(instructor.UserName);
        student = await _userManager.FindByNameAsync(student.UserName);

        if (instructor == null || student == null)
            throw new Exception("Instructor or student not found after creation");
        
        await _roleService.AddUserToRoleAsync(instructor.Id, "Instructor");
        await _roleService.AddUserToRoleAsync(student.Id, "Student");

        var course = new Course
        {
            Title = "Course 1",
            Description = "Course 1 description",
            InstructorId = instructor.Id
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var enrollment = new Enrollment
        {
            CourseId = course.Id,
            UserId = student.Id
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
    }

}