using Microsoft.AspNetCore.Identity;
using StudyMate.Domain.Entities;
using StudyMate.Infrastructure.Data;

namespace StudyMate.Infrastructure.SeedData;

public class DbSeeder
{
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public DbSeeder(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task Seed()
    {
        if (_context.Courses.Any() || _context.Enrollments.Any() || _context.Users.Any())
            return;

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