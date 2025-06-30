using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using StudyMate.Application.DTOs;
using StudyMate.Application.Interfaces;
using StudyMate.Application.Services;
using StudyMate.Domain.Entities;

namespace TestProject1;

public class AuthControllerTest
{
    [Fact]
    public async Task RegisterUser_ShouldReturnOk()
    {
        var dto = new RegisterDto
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            Password = "password"
        };

        var userManagerMock = new Mock<IUserManagerWrapper>();
        userManagerMock
            .Setup(u => u.CreateUserAsync(It.IsAny<User>(), dto.Password))
            .ReturnsAsync(IdentityResult.Success);
        
        var jwtTokenServiceMock = new Mock<IJwtTokenService>();
        
        var authService = new AuthServices(userManagerMock.Object, jwtTokenServiceMock.Object);

        var result = await authService.RegisterUserAsync(dto);
        
        result.Succeeded.ShouldBeTrue();
    }

    [Fact]
    public async Task RegisterUser_ShouldReturn_NotSuccessful()
    {
        var dto = new RegisterDto
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            Password = "password"
        };

        var resultFailed = IdentityResult.Failed(new IdentityError
        {
            Description = "Registration failed"
        });
        
        var userManagerMock = new Mock<IUserManagerWrapper>();
        userManagerMock
            .Setup(u => u.CreateUserAsync(It.IsAny<User>(), dto.Password))
            .ReturnsAsync(resultFailed);
        
        var jwtTokenServiceMock = new Mock<IJwtTokenService>();
        
        var authService = new AuthServices(userManagerMock.Object, jwtTokenServiceMock.Object);
        
        var result = await authService.RegisterUserAsync(dto);
        
        result.Succeeded.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Description == "Registration failed");
    }

    [Fact]
    public async Task LoginUser_ShouldReturn_Successful()
    {
        var testRole = new List<string> { "User" };
        var expectedToken = "mockToken";
        var testUser = new User
        {
            Email = "john.doe@gmail.com"
        };
        
        var userManagerMock = new Mock<IUserManagerWrapper>();
        userManagerMock
            .Setup(u => u.FindByNameAsync("john.doe@gmail.com"))
            .ReturnsAsync(testUser);
        
         userManagerMock
             .Setup(u => u.CheckPasswordAsync(testUser, "password"))
             .ReturnsAsync(true);
         
         userManagerMock
             .Setup(u => u.GetRolesAsync(testUser))
             .ReturnsAsync(testRole);
         
         var jwtTokenServiceMock = new Mock<IJwtTokenService>();
         jwtTokenServiceMock
             .Setup(j => j.GenerateTokenAsync(testUser, testRole))
             .Returns(expectedToken);
        
        var authService = new AuthServices(userManagerMock.Object, jwtTokenServiceMock.Object);

        var result = await authService.LoginAsync("john.doe@gmail.com", "password");
        
        result.ShouldBe(expectedToken);
    }
}