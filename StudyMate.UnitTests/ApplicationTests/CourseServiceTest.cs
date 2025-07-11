using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Domain.Entities;
using StudyMate.Application.Services;
using Shouldly;
using Moq;
using StudyMate.Application.DTOs;

namespace TestProject1.ApplicationTests;

public class CourseServiceTest
{
    private readonly Mock<ICourseRepository> _courseRepositoryMock;
    private readonly CourseService _courseService;

    public CourseServiceTest()
    {
        _courseRepositoryMock = new Mock<ICourseRepository>(); 
        _courseService = new CourseService(_courseRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsSuccessfully()
    {
        var list = new List<Course>
        {
            new Course { Description = "test", Id = 1, Price = 100},
            new Course { Description = "test", Id = 2, Price = 200}
        };
        
        _courseRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(list);
        
        var result = await _courseService.GetAllAsync();
        
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
        result.First().Description.ShouldBe("test");
        result.First().Price.ShouldBe(100);
        _courseRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsSuccessfully()
    {
        var course = new Course
        {
            Description = "test", Id = 1, Price = 100 
        };
        
        _courseRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(course);
        
        var result = await _courseService.GetByIdAsync(1);
        
        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        _courseRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNotSuccessfully()
    {
        _courseRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Course)null);
        
        var result = await _courseService.GetByIdAsync(1);
        
        result.ShouldBeNull();
        _courseRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ReturnsSuccessfully()
    {
        var course = new Course
        {
            Description = "test",
            Id = 1,
            Price = 100
        };

        var dto = new CourseCreateDto
        {
            Title = "test",
            Description = "test",
            Price = 100
        };
        
        _courseRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Course>()))
            .ReturnsAsync(course);
        
        var result = await _courseService.CreateAsync(dto);
        
        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        _courseRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsSuccessfully()
    {
        var course = new Course
        {
            Id = 1,
            Title = "test",
            Price = 100
        };

        var dto = new CourseUpdateDto
        {
            Id = course.Id,
            Price = course.Price,
            Title = course.Title
        };
        
        _courseRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(course);

        _courseRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<Course>()))
            .ReturnsAsync(course);
        
        var result = await _courseService.UpdateAsync(dto);
        
        result.ShouldNotBeNull();
        _courseRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Course>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsSuccessfully()
    {
        var userId = 1;
        
        _courseRepositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(true);
        
        var result = await _courseService.DeleteAsync(userId);
        result.ShouldBeTrue();
        _courseRepositoryMock.Verify(x => x.DeleteAsync(userId), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNotSuccessfully()
    {
        var userId = 1;
        
        _courseRepositoryMock
            .Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(false);
        
        var result = await _courseService.DeleteAsync(userId);
        result.ShouldBeFalse();
        _courseRepositoryMock.Verify(x => x.DeleteAsync(userId), Times.Once);
    }
}