using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using StudyMate.Application.DTOs;
using StudyMate.Application.DTOs.Auth;
using StudyMate.Application.Interfaces;
using StudyMate.Application.Interfaces.Services;
using StudyMate.Application.Services;
using StudyMate.Domain.Entities;

namespace TestProject1.ApplicationTests;

public class AuthServiceTest
{
    private readonly Mock<IUserManagerWrapper> _userManagerWrapper;
    private readonly Mock<IJwtTokenService> _jwtTokenService;
    private readonly AuthServices _authServices;

    public AuthServiceTest()
    {
        _userManagerWrapper = new Mock<IUserManagerWrapper>();
        _jwtTokenService = new Mock<IJwtTokenService>();
        _authServices = new AuthServices(_userManagerWrapper.Object, _jwtTokenService.Object);
    }
    
    [Fact]
    public async Task RegisterUser_ShouldReturn_Successful()
    {
        var dto = new RegisterDto
        {
            Name = "John Doe",
            Email = "john.doe@gmail.com",
            Password = "password"
        };
        
        _userManagerWrapper
            .Setup(u => u.CreateUserAsync(It.IsAny<User>(), dto.Password))
            .ReturnsAsync(IdentityResult.Success);

        var result = await _authServices.RegisterUserAsync(dto);
        
        result.Succeeded.ShouldBeTrue();
        result.ShouldNotBeNull();
        _userManagerWrapper.Verify(u => u.CreateUserAsync(It.IsAny<User>(), dto.Password), Times.Once);
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
        
        _userManagerWrapper
            .Setup(u => u.CreateUserAsync(It.IsAny<User>(), dto.Password))
            .ReturnsAsync(resultFailed);
        
        var result = await _authServices.RegisterUserAsync(dto);
        
        result.Succeeded.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.Description == "Registration failed");
        _userManagerWrapper.Verify(u => u.CreateUserAsync(It.IsAny<User>(), dto.Password), Times.Once);
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
        
        _userManagerWrapper
            .Setup(u => u.FindByNameAsync("john.doe@gmail.com"))
            .ReturnsAsync(testUser);
        
        _userManagerWrapper
             .Setup(u => u.CheckPasswordAsync(testUser, "password"))
             .ReturnsAsync(true);
         
        _userManagerWrapper
             .Setup(u => u.GetRolesAsync(testUser))
             .ReturnsAsync(testRole);
         
        _jwtTokenService
             .Setup(j => j.GenerateTokenAsync(testUser, testRole))
             .Returns(expectedToken);
        
        var result = await _authServices.LoginAsync("john.doe@gmail.com", "password");
        
        result.ShouldBe(expectedToken); 
        
        _userManagerWrapper.Verify(x => x.FindByNameAsync("john.doe@gmail.com"), Times.Once);
        _userManagerWrapper.Verify(x => x.GetRolesAsync(testUser), Times.Once);
        _userManagerWrapper.Verify(x => x.CheckPasswordAsync(testUser, "password"), Times.Once);
        _jwtTokenService.Verify(j => j.GenerateTokenAsync(testUser, testRole), Times.Once);
    }
}