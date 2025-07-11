using StudyMate.Application.Interfaces;
using StudyMate.Application.Services; 
using StudyMate.Infrastructure.Wrapper;
using StudyMate.Infrastructure.Repositories;
using StudyMate.Application.Interfaces.Repositories;
using StudyMate.Application.Interfaces.Services;
using StudyMate.Infrastructure.Services;
using StudyMate.Infrastructure.SeedData;

namespace StudyMate.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<DbSeeder>();
        
        services.AddScoped<IAuthService, AuthServices>();
        services.AddScoped<IUserManagerWrapper, UserManagerWrapper>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        
        return services;
    }
}