using StudyMate.Application.Interfaces.Repositories;
using Moq;
using Shouldly;
using StudyMate.Application.Services;
using StudyMate.Domain.Entities;

namespace TestProject1.ApplicationTests;

public class EnrollmentServiceTest
{
    private readonly Mock<IEnrollmentRepository> _enrollmentRepositoryMock;
    private readonly EnrollmentService _enrollmentService;

    public EnrollmentServiceTest()
    {
        _enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
        _enrollmentService = new EnrollmentService(_enrollmentRepositoryMock.Object);
    }

    [Fact]
    public async Task EnrollCourseAsync_ResultShouldBeSuccessful()
    {
        var course = new Course
        {
            Id = 1,
            Description = "Course 1",
            Price = 100
        };

        var user = new User
        {
            Id = "1",
            Email = "test@test.com",
            UserName = "test",
        };
        
        var courseId = 1;
        var studentId = user.Id;

        _enrollmentRepositoryMock
            .Setup(x => x.GetCourseByIdAsync(courseId))
            .ReturnsAsync(course);
        
        _enrollmentRepositoryMock
            .Setup(x => x.GetUserByIdAsync(studentId))
            .ReturnsAsync(user);
        
        _enrollmentRepositoryMock
            .Setup(x => x.IsAlreadyEnrolledAsync(courseId, studentId))
            .ReturnsAsync(false);

        _enrollmentRepositoryMock
            .Setup(x => x.AddEnrollmentAsync(It.IsAny<Enrollment>()))
            .ReturnsAsync(new Enrollment{ Id = 123});
        
        var result = await _enrollmentService.EnrollCourseAsync(courseId, studentId);

        result.ShouldNotBeNull();
        _enrollmentRepositoryMock.Verify(x => x.AddEnrollmentAsync(It.IsAny<Enrollment>()), Times.Once);
    }

    [Fact]
    public async Task EnrollCourseAsync_ResultShouldNotBeSuccessful()
    {
        var course = new Course
        {
            Id = 1,
            Description = "Course 1",
            Price = 100
        };

        var user = new User
        {
            Id = "1",
            Email = "test@test.com",
            UserName = "test",
        };
        
        var courseId = 1;
        var studentId = user.Id;

        _enrollmentRepositoryMock
            .Setup(x => x.GetCourseByIdAsync(courseId))
            .ReturnsAsync(course);
        
        _enrollmentRepositoryMock
            .Setup(x => x.GetUserByIdAsync(studentId))
            .ReturnsAsync(user);
        
        _enrollmentRepositoryMock
            .Setup(x => x.IsAlreadyEnrolledAsync(courseId, studentId))
            .ReturnsAsync(true);
        
        var result = await _enrollmentService.EnrollCourseAsync(courseId, studentId);

        result.ShouldBe("User already enrolled");
        _enrollmentRepositoryMock.Verify(x => x.AddEnrollmentAsync(It.IsAny<Enrollment>()), Times.Never);
    }

    [Fact]
    public async Task GetCoursesAsync_ShouldBeSuccessful()
    {
        var studentId = "1";
        
        var course = new List<Course>
        {
            new Course { Description = "test", Id = 1, Price = 100},
            new Course { Description = "test2", Id = 2, Price = 200}
        };

        _enrollmentRepositoryMock
            .Setup(x => x.GetCoursesByStudentIdAsync(studentId))
            .ReturnsAsync(course);

        var result = await _enrollmentService.GetCoursesAsync(studentId);
        
        result.ShouldNotBeNull();
        _enrollmentRepositoryMock.Verify(x => x.GetCoursesByStudentIdAsync(studentId), Times.Once);
    }

    [Fact]
    public async Task GetStudentsOfCourseAsync_ShouldBeSuccessful()
    {
        var courseId = 1;
        var requesterId = "test";

        var students = new List<User>
        {
            new User { Id = "1", Email = "test@test.com", UserName = "test" },
            new User { Id = "2", Email = "test2@test.com", UserName = "test2" }
        };
        
        _enrollmentRepositoryMock
            .Setup(x => x.GetStudentsOfCourseAsync(courseId))
            .ReturnsAsync(students);
        
        var result = await _enrollmentService.GetStudentsOfCourseAsync(courseId, requesterId);
        
        result.ShouldNotBeNull();
        _enrollmentRepositoryMock.Verify(x => x.GetStudentsOfCourseAsync(courseId), Times.Once);
    }
}