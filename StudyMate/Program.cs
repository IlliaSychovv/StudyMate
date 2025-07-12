using Microsoft.EntityFrameworkCore;
using StudyMate.Application.Options;
using StudyMate.Infrastructure.Data;
using StudyMate.Infrastructure.SeedData;
using StudyMate.Domain.Entities;
using StudyMate.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using Mapster;
using FluentValidation.AspNetCore;
using StudyMate.Application.Validator;
using StudyMate.Extensions;
using StudyMate.Extensions.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CourseCreateValidator>();
 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

TypeAdapterConfig.GlobalSettings.Scan(typeof(User).Assembly);
TypeAdapterConfig<Course, CourseDto>.NewConfig();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.Seed();
}

app.AddAuthEndpoints();
app.AddCourseEndpoints();
app.AddEnrollmentEndpoints();

app.Run(); 