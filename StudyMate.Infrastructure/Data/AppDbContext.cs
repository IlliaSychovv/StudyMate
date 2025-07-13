using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyMate.Domain.Entities;

namespace StudyMate.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Test>()
            .HasOne(e => e.Course)
            .WithMany()
            .HasForeignKey(e => e.CourseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<Question>()
            .HasOne(e => e.Test)
            .WithMany(t => t.Questions)
            .HasForeignKey(e => e.TestId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<TestResult>()
            .HasOne(e => e.Test)
            .WithMany(t => t.TestResults)
            .HasForeignKey(e => e.TestId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
}