using Microsoft.AspNetCore.Identity;

namespace StudyMate.Domain.Entities;

public class User : IdentityUser
{
    public UserRole Role { get; set; }
    
    public ICollection<Course>? Courses { get; set; } = new List<Course>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}